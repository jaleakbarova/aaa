using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Template.DAL;
using Template.Models;

namespace Template.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        private readonly IWebHostEnvironment _environment;
        public HomeController(AppDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}