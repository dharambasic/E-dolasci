using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Studenti.Models;
using Studenti.Utility;

namespace Studenti.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginModel> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
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

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            //CreateAdminIfDoesntExist();

            //CreateAdminIfDoesntExist();

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
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
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task<IActionResult> CreateAdminIfDoesntExist(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    Name = "Admin",
                    Surname = "Admin",
                    JMBAG = "111111111"
                };
                //var result = await _userManager.CreateAsync(user, "Admin1234");

                var result = await _signInManager.PasswordSignInAsync(user.Email, "Admin1234", false, false);

                if (!result.Succeeded)
                {
                    var creationResult = await _userManager.CreateAsync(user, "Admin1234");

                    if (creationResult.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync(StaticDetails.AdminRole))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(StaticDetails.AdminRole));
                        }

                        if (!await _roleManager.RoleExistsAsync(StaticDetails.ProfessorRole))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(StaticDetails.ProfessorRole));
                        }

                        if (!await _roleManager.RoleExistsAsync(StaticDetails.StudentRole))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(StaticDetails.StudentRole));
                        }

                        await _userManager.AddToRoleAsync(user, StaticDetails.AdminRole);

                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            //return null;
            return new OkResult();
        }

    }
}
