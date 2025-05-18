using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cineplex_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Cineplex_Management.Controllers;

public class HomeController : Controller
{
    private readonly CineplexContext db;
    public HomeController(CineplexContext db)
    {
        this.db = db;
    }
    public async Task<IActionResult> Index()
    {
        var movieList = await db.Movies.ToListAsync();
        return View(movieList);
    }
    public async Task<IActionResult> About()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
