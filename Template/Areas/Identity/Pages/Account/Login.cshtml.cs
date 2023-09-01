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

namespace Template.Areas.Identity.Pages.Account
{

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public class InputModel
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
        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            returnUrl ??= Url.Content("~/Common/Common/Home");
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ReturnUrl = returnUrl;
        }
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Common/Common/Home");
            if (!ModelState.IsValid) return Page();
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var signedUser = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
                var roles = await _signInManager.UserManager.GetRolesAsync(signedUser);
                //if (roles.Contains(Role.User))
                //{
                //    if (returnUrl == "/Controller/Home")
                //        returnUrl = Url.Content("~/User/Home/Index");
                //}
                // Remove existing claims
                var currentClaims = await _signInManager.UserManager.GetClaimsAsync(signedUser);
                currentClaims = currentClaims.Where(w => w.Type == "InstitutionId").ToList();
                if (currentClaims != null && currentClaims.Count() > 0)
                    await _signInManager.UserManager.RemoveClaimsAsync(signedUser, currentClaims);

                // Set Institution in claim principle
                //await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim("InstitutionId", signedUser.InstitutionId.ToString()));
                
                return LocalRedirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "Incorrect email address and / or password.");
            return Page();
        }
    }

    //public class LoginModel : PageModel
    //{
    //    private readonly SignInManager<ApplicationUser> _signInManager;
    //    private readonly ILogger<LoginModel> _logger;

    //    public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
    //    {
    //        _signInManager = signInManager;
    //        _logger = logger;
    //    }

    //    /// <summary>
    //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //    ///     directly from your code. This API may change or be removed in future releases.
    //    /// </summary>
    //    [BindProperty]
    //    public InputModel Input { get; set; }

    //    /// <summary>
    //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //    ///     directly from your code. This API may change or be removed in future releases.
    //    /// </summary>
    //    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    //    /// <summary>
    //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //    ///     directly from your code. This API may change or be removed in future releases.
    //    /// </summary>
    //    public string ReturnUrl { get; set; }

    //    /// <summary>
    //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //    ///     directly from your code. This API may change or be removed in future releases.
    //    /// </summary>
    //    [TempData]
    //    public string ErrorMessage { get; set; }

    //    /// <summary>
    //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //    ///     directly from your code. This API may change or be removed in future releases.
    //    /// </summary>
    //    public class InputModel
    //    {
    //        /// <summary>
    //        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //        ///     directly from your code. This API may change or be removed in future releases.
    //        /// </summary>
    //        [Required]
    //        [EmailAddress]
    //        public string Email { get; set; }

    //        /// <summary>
    //        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //        ///     directly from your code. This API may change or be removed in future releases.
    //        /// </summary>
    //        [Required]
    //        [DataType(DataType.Password)]
    //        public string Password { get; set; }

    //        /// <summary>
    //        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    //        ///     directly from your code. This API may change or be removed in future releases.
    //        /// </summary>
    //        [Display(Name = "Remember me?")]
    //        public bool RememberMe { get; set; }
    //    }

    //    public async Task OnGetAsync(string returnUrl = null)
    //    {
    //        if (!string.IsNullOrEmpty(ErrorMessage))
    //        {
    //            ModelState.AddModelError(string.Empty, ErrorMessage);
    //        }

    //        returnUrl ??= Url.Content("~/");

    //        // Clear the existing external cookie to ensure a clean login process
    //        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

    //        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

    //        ReturnUrl = returnUrl;
    //    }

    //    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    //    {
    //        returnUrl ??= Url.Content("~/");

    //        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

    //        if (ModelState.IsValid)
    //        {
    //            // This doesn't count login failures towards account lockout
    //            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
    //            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
    //            if (result.Succeeded)
    //            {
    //                _logger.LogInformation("User logged in.");
    //                return LocalRedirect(returnUrl);
    //            }
    //            //if (result.RequiresTwoFactor)
    //            //{
    //            //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
    //            //}
    //            if (result.IsLockedOut)
    //            {
    //                _logger.LogWarning("User account locked out.");
    //                return RedirectToPage("./Lockout");
    //            }
    //            else
    //            {
    //                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    //                return Page();
    //            }
    //        }

    //        // If we got this far, something failed, redisplay form
    //        return Page();
    //    }
    //}
}



