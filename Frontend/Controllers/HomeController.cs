using System.Diagnostics;
using kds.Models;
using Microsoft.AspNetCore.Mvc;

namespace kds.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult POS()
    {
        return View();
    }

    public IActionResult Station()
    {
        return View();
    }

    public IActionResult Client()
    {
        return View();
    }


    public IActionResult Settings()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
