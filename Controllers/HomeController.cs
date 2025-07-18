using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThomasConstruction.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ThomasConstruction.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {

         var projects = await _context.Projects.ToListAsync();

    var projectNames = projects.Select(p => p.project_name).ToList();
    var profits = projects.Select(p => p.profit).ToList();
    var costs = projects.Select(p => p.cost).ToList();

    ViewBag.ProjectNames = JsonConvert.SerializeObject(projectNames);
    ViewBag.Profits = JsonConvert.SerializeObject(profits);
    ViewBag.Costs = JsonConvert.SerializeObject(costs);

    return View(projects); 
       // return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
