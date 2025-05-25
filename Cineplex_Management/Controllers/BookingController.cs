using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cineplex_Management.Models;

namespace Cineplex_Management.Controllers
{
    public class BookingController : Controller
    {
        private readonly CineplexContext _context;

        public BookingController(CineplexContext context)
        {
            _context = context;
        }

        // GET: Booking/SelectShow
        // GET: Booking/SelectShow
        public async Task<IActionResult> SelectShow()
        {
            try
            {
                var currentDate = DateTime.Today;
                Console.WriteLine($"Current date: {currentDate}");

                var shows = await _context.Shows
                    .Include(s => s.Movie)
                    .Include(s => s.Hall)
                    .Where(s => s.ShowStart <= currentDate && s.ShowEnd >= currentDate)
                    .OrderBy(s => s.ShowStart)
                    .ToListAsync();

                Console.WriteLine($"Found {shows.Count} shows");
                if (shows.Any())
                {
                    Console.WriteLine($"First show: {shows[0].ShowName} ({shows[0].ShowStart} to {shows[0].ShowEnd})");
                }

                return View(shows);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SelectShow: {ex.Message}");
                // Return empty list if there's an error
                return View(new List<Show>());
            }
        }

        // GET: Booking/SelectSeat/{showId}
        public async Task<IActionResult> SelectSeat(int showId)
        {
            var show = await _context.Shows
                .Include(s => s.Hall)
                .ThenInclude(h => h.SeatPlans)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.ShowId == showId);

            if (show == null || show.Hall == null)
            {
                return NotFound();
            }

            // Get already booked seats for this show
            var bookedSeats = await _context.ShowDetails
                .Where(sd => sd.ShowId == showId && sd.IsBooked == true)
                .Select(sd => sd.SeatNumber)
                .ToListAsync();

            ViewBag.BookedSeats = bookedSeats;
            return View(show);
        }

        // POST: Booking/BookSeats
        [HttpPost]
        public async Task<IActionResult> BookSeats(int showId, List<string> selectedSeats)
        {
            if (selectedSeats == null || !selectedSeats.Any())
            {
                TempData["Error"] = "Please select at least one seat";
                return RedirectToAction(nameof(SelectSeat), new { showId });
            }

            // Get the current user ID
            var customerId = GetCurrentCustomerId();
            if (customerId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var show = await _context.Shows
                .Include(s => s.Hall)
                .ThenInclude(h => h.SeatPlans)
                .FirstOrDefaultAsync(s => s.ShowId == showId);

            if (show == null || show.Hall == null)
            {
                return NotFound();
            }

            // Check if seats are still available
            var alreadyBooked = await _context.ShowDetails
                .Where(sd => sd.ShowId == showId && selectedSeats.Contains(sd.SeatNumber) && sd.IsBooked == true)
                .AnyAsync();

            if (alreadyBooked)
            {
                TempData["Error"] = "Some selected seats are no longer available. Please select different seats.";
                return RedirectToAction(nameof(SelectSeat), new { showId });
            }

            // Calculate total price
            decimal totalPrice = 0;
            foreach (var seat in selectedSeats)
            {
                var seatPlan = show.Hall.SeatPlans.FirstOrDefault(sp => sp.SeatNumber == seat);
                totalPrice += seatPlan?.Price ?? 0;
            }

            // Book the seats
            foreach (var seat in selectedSeats)
            {
                var showDetail = new ShowDetail
                {
                    ShowId = showId,
                    HallId = show.Hall.HallId,
                    SeatNumber = seat,
                    IsBooked = true,
                    CustomerId = int.Parse(customerId)
                };

                _context.ShowDetails.Add(showDetail);
            }

            await _context.SaveChangesAsync();

            // Store the total price in TempData to display in confirmation
            TempData["TotalPrice"] = totalPrice;

            return RedirectToAction(nameof(BookingConfirmation), new { showId });
        }

        // GET: Booking/BookingConfirmation/{showId}
        public async Task<IActionResult> BookingConfirmation(int showId)
        {
            var show = await _context.Shows
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .ThenInclude(h => h.SeatPlans)
                .FirstOrDefaultAsync(s => s.ShowId == showId);

            if (show == null)
            {
                return NotFound();
            }

            // Get the latest booking for this customer
            var customerId = GetCurrentCustomerId();
            var bookedSeats = await _context.ShowDetails
                .Where(sd => sd.ShowId == showId && sd.CustomerId == int.Parse(customerId) && sd.IsBooked == true)
                .OrderByDescending(sd => sd.CustomerId)
                .Select(sd => sd.SeatNumber)
                .ToListAsync();

            // Calculate total price from TempData or recalculate
            var totalPrice = TempData["TotalPrice"] as decimal? ?? 0m;
            if (totalPrice == 0)
            {
                foreach (var seat in bookedSeats)
                {
                    var seatPlan = show.Hall?.SeatPlans?.FirstOrDefault(sp => sp.SeatNumber == seat);
                    totalPrice += seatPlan?.Price ?? 0;
                }
            }

            ViewBag.BookedSeats = bookedSeats;
            ViewBag.TotalPrice = totalPrice;
            return View(show);
        }

        private string GetCurrentCustomerId()
        {
            return User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }
    }
}