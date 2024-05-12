using ECommerceShoppingApplication.Data;
using ECommerceShoppingApplication.Models;
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
            if (ModelState.IsValid)
            {
                var userExists = _userManager.FindByNameAsync(userModel.UserName).Result;
                if (userExists != null)
                {
                    ModelState.AddModelError(string.Empty, "User already exists!");
                    return View(userModel);
                }

                var user = new ApplicationUser()
                {
                    UserName = userModel.UserName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };

                var result = _userManager.CreateAsync(user, userModel.PasswordHash).Result;
                if (!result.Succeeded)
                {
                    return View(userModel);
                }

                if (!_roleManager.RoleExistsAsync(userModel.Role).Result)
                    await _roleManager.CreateAsync(new IdentityRole(userModel.Role));


                if (_roleManager.RoleExistsAsync(userModel.Role).Result)
                {
                    await _userManager.AddToRoleAsync(user, userModel.Role);
                }

                TempData["Success"] = "You have registered successfully";

            }


            //if (!ModelState.IsValid) { return View(userModel); }
            //var result = await RegisterAsync(userModel);
            //TempData["msg"] = result;
            return View(nameof(SignUp));
        }


        public async Task<bool>  RegisterAsync(RegisterUser model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return false;
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, model.PasswordHash);
            if (!result.Succeeded)
            {
                return false;
            }

            if (!await _roleManager.RoleExistsAsync(model.Role))
                await _roleManager.CreateAsync(new IdentityRole(model.Role));


            if (await _roleManager.RoleExistsAsync(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            return true;
        }
    }
}
