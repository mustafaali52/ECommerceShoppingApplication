using ECommerceShoppingApplication.Data;
using ECommerceShoppingApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;

namespace ECommerceShoppingApplication.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDBContext _db;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public AccountController(ApplicationDBContext db, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _db = db;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager; 
        }
        
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(user.UserName, 
                    user.Password, false, false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");   
                }
                ModelState.AddModelError(string.Empty, "Invalid user name/or password!");
            }
            return View(user);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp (RegisterUser userModel)
        {
            if (ModelState.IsValid) {
                var userExists = _userManager.FindByNameAsync(userModel.UserName).Result;    
                if (userExists!= null)
                {
                    ModelState.AddModelError(string.Empty, "User already exists!");
                    return View(userModel);
                }

                var user = new ApplicationUser
                {
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                };

                var result = _userManager.CreateAsync(user, userModel.PasswordHash).Result;
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(userModel.Role).Result)
                       await _roleManager.CreateAsync(new IdentityRole(userModel.Role));

                    if (_roleManager.RoleExistsAsync(userModel.Role).Result)
                        await _userManager.AddToRoleAsync(user, userModel.Role);

                    TempData["Success"] = "You have registered successfully";

                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(userModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
