using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Template.DAL;
using Template.Models;
using Template.Utilities.Extension;

namespace Template.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        private readonly IWebHostEnvironment _environment;
        public BlogController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            
            return View(_context.Blog.ToList());
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {


            if (!ModelState.IsValid) return View(blog);

            if (!blog.ImageFile.CheckFileType("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be image format");
                return View();
            }

            if (blog.ImageFile.CheckFileSize(2000))
            {
                ModelState.AddModelError("ImageFile", "File must be less than 200Kb");
                return View();

            }

            blog.Image = await blog.ImageFile.SaveFileAsync(_environment.WebRootPath, "assets/img");
            await _context.Blog.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        //----------------------------------------------EDIT-------------------------------------------------//


        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Blog.FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Blog blog)
        {
            Blog ? exists = await _context.Blog.FirstOrDefaultAsync(x => x.Id == blog.Id);

            if (exists == null)
            {
                ModelState.AddModelError("", "Blog not found");
                return View(blog);
            }

            if (blog.ImageFile != null)
            {
                if (!blog.ImageFile.CheckFileType("image"))
                {
                    ModelState.AddModelError("ImageFile", "File must be image format");
                    return View();
                }
                if (blog.ImageFile.CheckFileSize(200))
                {
                    ModelState.AddModelError("ImageFile", "File must be less than 200kb");
                    return View();
                }
                exists.Image = await blog.ImageFile.SaveFileAsync(
                    _environment.WebRootPath, "assets/img");
            }

            exists.Title = blog.Title;
            exists.Description = blog.Description;
            exists.Image = blog.Image;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //---------------------------------------------DELETE-------------------------------------------//



        public async Task<IActionResult> Delete(int id)
        {
            Blog? exist = await _context.Blog.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null)
            {
                return Json(new { status = 404 });
            }

            _context.Blog.Remove(exist);
            await _context.SaveChangesAsync();


            return Json(new { status = 200 });
        }
    }
}

