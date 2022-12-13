/**
 * Ryan Boldy
 * NETD
 * 12/12/2022
 * Lab 5
 */

//Inclusions
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Make sure you have the following imports
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Lab5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;


namespace Lab5.Controllers

{
    //Controller for all acount related views/viewmodels
    public class AccountController : Controller
    {
        //Index
        public IActionResult Index()
        {
            return View();
        }

        //Managers
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger _logger;

        //Manager assignment
        public AccountController(
                    UserManager<AppUser> userManager,
                    SignInManager<AppUser> signInManager,
                    ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }



        #region Helpers
        //Helpers (auto-generated)
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
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

        #endregion

        //Register get view
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //register post view
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            //Gets the url to return to previous page
            ViewData["ReturnUrl"] = returnUrl;
            //If the modelstate is valid, do the register
            if (ModelState.IsValid)
            {
                //Generate new AppUser
                var user = new AppUser { UserName = model.Email, Email = model.Email };
                //Create the new user
                var result = await _userManager.CreateAsync(user, model.Password);
                
                //Log & Redirect
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            //Re-displays page on failure
            return View(model);
        }
        //Login get view
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //Display page and remember returnURL
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //Login post
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            //remember return url
            ViewData["ReturnUrl"] = returnUrl;
            //If all form data is valid
            if (ModelState.IsValid)
            {
                //No lockout, attempts to sign in with the information from the model
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {            
                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // Redisplay form on error
            return View(model);
        }

        //Lockout unused.

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        //Get logout
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            //Signs out and returns view
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //Signs out and returns user to home.
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //Get Forgot Password
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        //Post forgot password
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel pswd)
        {
            //If form is valid
            if (ModelState.IsValid)
            {
                //If the user is found with the email
                var found_user = await _userManager.FindByEmailAsync(pswd.Email);
                if (found_user != null)
                {
                    //Then generate the token
                    var password_token = await _userManager.GeneratePasswordResetTokenAsync(found_user);
                    //Generate the URL with the token
                    var reset = Url.Action("ResetPassword", "Account", new { email = pswd.Email, token = password_token }, Request.Scheme);
                    //Log the url to the debug console
                    _logger.LogInformation(reset);
                }
                //Log reset failed to console
                else { _logger.LogInformation("Reset failed"); }
                
                //Return the success view
                return View("Confirm");
            }
            //If error, re-display the reset password view
            return View(pswd);
        }

        //Get for password reset
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            //Only used to a) load the page under get mode and b) to grab token and email as get variables from the url
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            //If the form is valid
            if (ModelState.IsValid)
            {
                //Find the user by the email
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    //If user exists, update their password
                    await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                }
                return View("ResetSuccess");

            }
            //If this has been reached, there has been an error
            return View(model);
        }

    }
}
