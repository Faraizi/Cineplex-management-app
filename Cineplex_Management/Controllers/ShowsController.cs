using Cineplex_Management.Models;
using Cineplex_Management.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Data;
namespace Cineplex_Management.Controllers
{
    public class ShowsController : Controller
    {
        CineplexContext _ctx;
        public ShowsController(CineplexContext ctx)
        {
            _ctx = ctx;
        }


        // GET: ShowsController
        public ActionResult Index()
        {
            var showsWithDetails = _ctx.Shows
                .Include(s => s.Hall) // Assuming 'Hall' is a navigation property in your Show entity
                .Include(s => s.Movie) // Assuming 'Movie' is a navigation property in your Show entity
                .ToList();

            return View(showsWithDetails);
        }
        // GET: ShowsController/Details/5
        public ActionResult Details(int id)
        {
            
                var showsWithDetails = _ctx.Shows
               .Include(s => s.Hall) // Assuming 'Hall' is a navigation property in your Show entity
               .Include(s => s.Movie) // Assuming 'Movie' is a navigation property in your Show entity
               .ToList();
                return View(showsWithDetails);

        }

        // GET: ShowsController/Create
        public ActionResult Create()
        {
            var listMovie = _ctx.Movies.ToList();
            var movies = new List<SelectListItem>();
            foreach (var oMovie in listMovie) 
            {
                var movie = new SelectListItem();
                movie.Value = oMovie.MovieId.ToString();
                movie.Text = oMovie.MovieName;
                movies.Add(movie);
            }
            var listHall = _ctx.Halls.ToList();
            var halls = new List<SelectListItem>();
            foreach (var oHall in listHall)
            {
                var hall = new SelectListItem();
                hall.Value = oHall.HallId.ToString();
                hall.Text = oHall.HallName;
                halls.Add(hall);
            }
            var model = new ShowVm();
            model.Movies = movies;
            model.Halls = halls;
            return View(model);
        }

        // POST: ShowsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new Show();
                model.ShowStart = Convert.ToDateTime(collection["ShowStart"]);
                model.ShowEnd = Convert.ToDateTime(collection["ShowEnd"]);
                model.ShowName = Convert.ToString(collection["ShowName"]);
                model.HallId = Convert.ToInt32(collection["HallId"]);
                model.MovieId = Convert.ToInt32(collection["MovieId"]);
                _ctx.Shows.Add(model);
                _ctx.SaveChanges();
                var listShowDetail = new List<ShowDetail>();
                var listSeat = (from x in _ctx.SeatPlans where x.HallId == model.HallId select x).ToList();
                foreach (var seat in listSeat) 
                {
                    var oShowDetail = new ShowDetail();
                    oShowDetail.IsBooked = false;
                    oShowDetail.HallId = model.HallId;
                    oShowDetail.CustomerId = null;
                    oShowDetail.SeatNumber = null;
                    oShowDetail.ShowId = model.ShowId;

                    listShowDetail.Add(oShowDetail);
                }
                _ctx.ShowDetails.AddRange(listShowDetail);
                _ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShowsController/Edit/5
        public ActionResult Edit(int id)
        {
            var listMovie = _ctx.Movies.ToList();
            var movies = new List<SelectListItem>();
            foreach (var oMovie in listMovie)
            {
                var movie = new SelectListItem();
                movie.Value = oMovie.MovieId.ToString();
                movie.Text = oMovie.MovieName;
                movies.Add(movie);
            }
            var listHall = _ctx.Halls.ToList();
            var halls = new List<SelectListItem>();
            foreach (var oHall in listHall)
            {
                var hall = new SelectListItem();
                hall.Value = oHall.HallId.ToString();
                hall.Text = oHall.HallName;
                halls.Add(hall);
            }
            var model = new ShowVm();
            model.Movies = movies;
            model.Halls = halls;
            var oShow = (from x in _ctx.Shows where x.ShowId == id select x).FirstOrDefault();
            if (oShow != null)
            {
                model.ShowStart = oShow.ShowStart;
                model.ShowEnd = oShow.ShowEnd;
                model.ShowId = oShow.ShowId;    
                model.ShowName = oShow.ShowName;
                model.HallId = oShow.HallId;
                model.MovieId = oShow.MovieId;
            }
            return View(model);
        }

        // POST: ShowsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var model = (from x in _ctx.Shows where x.ShowId == id select x).FirstOrDefault();
                if (model != null) 
                {
                    model.ShowStart = Convert.ToDateTime(collection["ShowStart"]);
                    model.ShowEnd = Convert.ToDateTime(collection["ShowEnd"]);
                    model.ShowName = Convert.ToString(collection["ShowName"]);
                    model.HallId = Convert.ToInt32(collection["HallId"]);
                    model.MovieId = Convert.ToInt32(collection["MovieId"]);
                    
                    _ctx.SaveChanges();

                    var listShowDetailRem = (from x in _ctx.ShowDetails where x.ShowId == model.ShowId select x).ToList();
                    if (listShowDetailRem.Count > 0)
                    {
                        _ctx.ShowDetails.RemoveRange(listShowDetailRem);
                        _ctx.SaveChanges();
                    }

                    var listShowDetail = new List<ShowDetail>();
                    var listSeat = (from x in _ctx.SeatPlans where x.HallId == model.HallId select x).ToList();
                    foreach (var seat in listSeat)
                    {
                        var oShowDetail = new ShowDetail();
                        oShowDetail.IsBooked = false;
                        oShowDetail.HallId = model.HallId;
                        oShowDetail.CustomerId = null;
                        oShowDetail.SeatNumber = null;
                        oShowDetail.ShowId = model.ShowId;

                        listShowDetail.Add(oShowDetail);
                    }
                    _ctx.ShowDetails.AddRange(listShowDetail);
                    _ctx.SaveChanges();
                }
                
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShowsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShowsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
