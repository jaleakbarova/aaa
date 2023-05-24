using Template.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.ViewModels;
using Template.View_Models;

namespace Template.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppUser> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppUser> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVm)
        {
            if (!ModelState.IsValid) return View();

            AppUser newUser = new AppUser()
            {
                Name = registerVm.Name,
                Surname = registerVm.Surname,
               
                Email = registerVm.Email
            };

            var result = await _userManager.CreateAsync(newUser, registerVm.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }

            await _signInManager.SignInAsync(newUser, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null) return View();
        //    var result = await _userManager.ConfirmEmailAsync(user, token);
        //    if (result.Succeeded)
        //    {
        //        await _signInManager.SignInAsync(user, false);
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View();
        //}

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {
            if (!ModelState.IsValid) return View();

            if (loginVm.UsernameOrEmail.Contains("@"))
            {
                var user = await _userManager.FindByEmailAsync(loginVm.UsernameOrEmail);

                if (user == null) return NotFound();

                var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, false);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "invalid credentals");
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }

            else
            {
                var user = await _userManager.FindByNameAsync(loginVm.UsernameOrEmail);

                if (user == null) return NotFound();

                var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, false);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "invalid credentals");
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}