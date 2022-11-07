﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using NJInsurancePlatform.Models;

using Microsoft.AspNetCore.Authorization;

namespace NJInsurancePlatform.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                // assign new record to "user" 
                // assign value from input field
                var user = new ApplicationUser { UserName = model.UserName };

                // Create new record in "(user, model.Password)". user = UserName, model.Password = Password
                var result = await userManager.CreateAsync(user, model.Password);

                // If Creation is Successful
                if (result.Succeeded)
                {
                    // Sign allow access. in "(isPersistent: false)" Means remove Cookie On sign out
                    await signInManager.SignInAsync(user, isPersistent: false);

                    if (!User.IsInRole("Customer")){
                        await userManager.AddToRoleAsync(user, "Customer");
                    }

                    return RedirectToAction("Index", "Home");
                }

                // If creation is NOT successful,
                // Password Exceptions Will appear in red
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Sign in with "signInManager". 
                // Get UserName and Password from View Input fields
                // is Persistent will be false. (We don't want to save Cookie)
                // last false is to prevent lockout if credentials are incorrect.
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, false);


                // If Login is Successful
                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(model.UserName);           // Assign UserName to a variable for the purpose Of checking Role Status Later

                    // If Admin Signs In, Redirect To Roles Page
                    if (await userManager.IsInRoleAsync(user, "Admin")) return RedirectToAction("GetRoles", "Administration");
                    return RedirectToAction("MyPage", "Customer");
                }

                // If Model is not valid
                ModelState.AddModelError(String.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
}