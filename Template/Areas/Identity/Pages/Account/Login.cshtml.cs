// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Template.Areas.Identity.Data;
using Template.Views.Shared.Constants;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace Template.Areas.Identity.Pages.Account
{

    [AllowAnonymous]
    public class LoginRegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;


        public LoginRegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public LoginModel Login { get; set; }
        [BindProperty]
        public RegisterModel Register { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }


        public class LoginModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }


        public class RegisterModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

        }

        public async Task OnGetAsyncLogin(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task OnGetAsyncRegister(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Home");

            // Login
            if (!string.IsNullOrEmpty(Login.Email) && !string.IsNullOrEmpty(Login.Password))
            {
                var signedInUser = await _signInManager.UserManager.FindByEmailAsync(Login.Email);
                var role = await _signInManager.UserManager.GetRolesAsync(signedInUser);

                if (role.Contains(Role.User))
                {
                    returnUrl = Url.Content("~/HomeFrontEnd/Index");
                }
                else if (role.Contains(Role.SuperAdmin))
                {
                    returnUrl = Url.Content("~/Home");
                }

                var result = await _signInManager.PasswordSignInAsync(Login.Email, Login.Password, Login.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            else if (!string.IsNullOrEmpty(Register.Email) && !string.IsNullOrEmpty(Register.Password) && !string.IsNullOrEmpty(Register.ConfirmPassword))
            {
                // Register
                if (Register.Password != Register.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "The password and confirmation password do not match.");
                }
                else
                {
                    
                    var user = CreateUser();
                    await _userStore.SetUserNameAsync(user, Register.Email, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, Register.Email, CancellationToken.None);

                    user.IsDeleted = false;
                    var result = await _userManager.CreateAsync(user, Register.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        returnUrl = Url.Content("~/HomeFrontEnd/Index");
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        // Handle registration failure
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            // If no successful login or registration occurred, stay on the same page
            return Page();
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

    }
}



