using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kds.Models;

namespace kds.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _iProductService;
    private readonly ICategoryService _iCategoryService;

    public HomeController(
        ILogger<HomeController> logger,
        IProductService iProductService,
        ICategoryService iCategoryService)
    {
        _logger = logger;
        _iProductService = iProductService;
        _iCategoryService = iCategoryService;
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
