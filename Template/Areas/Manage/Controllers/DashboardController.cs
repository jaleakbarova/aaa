using Microsoft.AspNetCore.Mvc;
using Template.DAL;

namespace Template.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        private readonly IWebHostEnvironment _environment;
        public DashboardController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
