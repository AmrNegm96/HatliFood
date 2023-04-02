using HatliFood.Data;
using HatliFood.Models;
using HatliFood.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace HatliFood.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _Context;

        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext Context, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _Context = Context;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            return RedirectToAction("AdminHome", "Home");
                        }

                        if (await _userManager.IsInRoleAsync(user, "User"))
                        {
                            return RedirectToAction("AllRestaurants", "Restaurants");
                        }
                        if (await _userManager.IsInRoleAsync(user, "Delivery"))
                        {
                            return RedirectToAction("Index", "OrdersDel");
                        }
                        if (await _userManager.IsInRoleAsync(user, "Kitchen"))
                        {
                            return RedirectToAction("Index", "OrdersDel");
                        }
                    }
                }
                TempData["Error"] = "Wrong crendentials ,  try again!";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong crendentials ,  try again!";
            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("AllRestaurants", "Restaurants");
        }


        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This Email address is already in use";
                return View(registerVM);
            }

            var newUser = new IdentityUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await _roleManager.RoleExistsAsync(UserRoles.Kitchen))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Kitchen));
                if (!await _roleManager.RoleExistsAsync(UserRoles.Delivery))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Delivery));

                /////Error
                var roleExists = await _roleManager.RoleExistsAsync(UserRoles.User);
                if (!roleExists)
                {
                    var newRole = new IdentityRole(UserRoles.User);
                    await _roleManager.CreateAsync(newRole);
                }
                /////
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

                var newBuyer = new Buyer()
                {
                    UserId = newUser.Id,
                    FirstName = registerVM.FisrtName,
                    LastName = registerVM.LastName
                };
                _Context.Buyers.Add(newBuyer);
                _Context.SaveChanges();
            }

            return View("RegisterCompleted");
        }

        [HttpGet]
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // If the user doesn't exist, create a new one
                user = new ApplicationUser { UserName = "YoussefEhab", Email = email };
                var result1 = await _userManager.CreateAsync(user);
                if (!result1.Succeeded)
                {
                    foreach (var error in result1.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View("Login");
                }
            }


            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email1 = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new LoginVM { EmailAddress = email1 });
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


    }
}
