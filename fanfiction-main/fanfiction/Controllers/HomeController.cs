    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using fanfiction.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using fanfiction.Models.User;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using EmailApp;
    using Microsoft.AspNetCore.Mvc;
    using fanfiction.Data;
    using Microsoft.AspNetCore.CookiePolicy;


    namespace fanfiction.Controllers
    {
        [Authorize]
        public class HomeController : Controller
        {
            private readonly UserManager<ApplicationUser> _userManager;

            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private ApplicationDbContext _context;
            public HomeController(ApplicationDbContext context,RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
            {
                this._context = context;
                this._roleManager = roleManager;
                this._userManager = userManager;
                this._signInManager = signInManager;
            }

            [HttpGet]
            [AllowAnonymous]
            public async Task<IActionResult> SignInAsync(string returnUrl = null)
            {
                await CreateRole();
                if (!_signInManager.IsSignedIn(User)) TempData["n"] = 223;
                return View(new UserLog {ReturnUrl = returnUrl});
            }
            async Task CreateRole()
            {
                if (await _roleManager.RoleExistsAsync("Admin") == false)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if(await _roleManager.RoleExistsAsync("User") == false)
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
            }

            [HttpGet]
            [AllowAnonymous]
            public IActionResult SignUp()
            {
                return View();
            }


            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> SignUp(UserReg userReg)
            {
                var msg = await Task.Run(() => Checker.checkRegistrarion(_userManager, userReg));
                if (msg == string.Empty)
                {
                    var user = new ApplicationUser(userReg);
                    var result = await _userManager.CreateAsync(user, userReg.Password);
                    if (result.Succeeded)
                    {
                        if (user.Email == "boss.selyavko@mail.ru")
                        {
                            await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(user.Email), "Admin");
                 
                        }
                        await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(user.Email), "User");
                        return View("SignIn");
                    }
                
                    foreach (var i in result.Errors) msg += i.Description;
                }

                TempData["SignUpError"] = msg;
                return View();
            }




            [HttpPost]
            [ValidateAntiForgeryToken]
            [AllowAnonymous]
            public async Task<IActionResult> SignIn(UserLog userLog)
            {
            
                try 
                {
                
                    var user = await _userManager.FindByEmailAsync(userLog.Email);
                    if(user == null)
                    {
                        return View(userLog);
                    }
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, userLog.Password, false, false);
                    if (result.Succeeded)
                    {


                        return RedirectToAction("Profile", "Profile");

                    }
                
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.ToString(), ex);
                }
            if (!_signInManager.IsSignedIn(User)) TempData["SignInError"] = "Incorrect data";


            return View(userLog);
            }

            private void DeleteCookie()
            {
                Response.Cookies.Delete("lang");
                Response.Cookies.Delete("theme");
            }
            private async Task SetStatus(ApplicationUser user)
            {
                if(EmailService.getAdminByEmail(user.Email))
                {
                   await _userManager.AddToRoleAsync(user, "Admin");
                }
                else 
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                await _userManager.UpdateAsync(user);
            }
        
       
        
       
        
        }
    }