using Cineplex_Management.Models;
using Cineplex_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace Cineplex_Management.Controllers
{
    public class MovieController : Controller
    {
        private readonly CineplexContext db;
        private readonly IWebHostEnvironment en;

        // Constructor to initialize the context and environment
        public MovieController(CineplexContext db, IWebHostEnvironment en)
        {
            this.db = db;
            this.en = en;
        }

        // Movie Details page with hall and show details
        //public ActionResult Details(int id)
        //{
        //    var mv = db.Movies.Include(sd=>sd.Shows).Include(h=>h.Hall).Include(m=>m.Movie);
        //    return View(mv);
        //}
        // Index action to display the list of movies
        public async Task<IActionResult> Index()
        {
            var movieList = await db.Movies.ToListAsync();
            return View(movieList);
        }

        // GET: Create action to show the form
        [HttpGet]
        public IActionResult Create()
        {
            return View(new MovieVM());
        }

        // POST: Create action to handle form submission and image upload
        [HttpPost]
        public async Task<IActionResult> Create(MovieVM movieVM)
        {
            if (ModelState.IsValid)
            {
                // Create a new movie object
                Movie movie = new Movie
                {
                    MovieId = movieVM.MovieId,
                    MovieName = movieVM.MovieName,
                    ProducerId = movieVM.ProducerId
                };

                // Handling image upload
                if (movieVM.ImageVm != null && movieVM.ImageVm.Length > 0)
                {
                    // Generate a unique filename using Guid to avoid name conflicts
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(movieVM.ImageVm.FileName);
                    string filePath = Path.Combine(en.WebRootPath, "images", uniqueFileName);

                    // Save the image to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await movieVM.ImageVm.CopyToAsync(fileStream);  // Use await for async file copy
                    }

                    // Set the relative path to the image field in the movie object
                    movie.Image = "/images/" + uniqueFileName;
                }

                // Add the movie to the database
                db.Movies.Add(movie);
                await db.SaveChangesAsync();  // Save changes asynchronously
                return RedirectToAction("Index");
            }

            // If validation fails, return to the Create view with the data
            return View(movieVM);
        }

        // Delete action to handle image deletion and movie removal
        [HttpPost]  // Make sure the delete action uses POST method to avoid accidental deletes
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await db.Movies.FindAsync(id);
            if (movie != null)
            {
                // Delete the image file from the server if it exists
                if (!string.IsNullOrEmpty(movie.Image))
                {
                    string imagePath = Path.Combine(en.WebRootPath, movie.Image.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);  // Delete the file from the server
                    }
                }

                // Remove movie from the database
                db.Movies.Remove(movie);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return NotFound();  // If movie not found, return a 404 page
        }
    }
}
