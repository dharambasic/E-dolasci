using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Studenti.Data;
using Studenti.Models;
using Studenti.Utility;

namespace Studenti.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    //[Authorize(Roles = StaticDetails.AdminRole)]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly UserManager<Korisnik> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<Korisnik> userManager,
            SignInManager<Korisnik> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext db,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _db = db;
            _roleManager = roleManager;
            
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public List<SelectListItem> RoleList;

        public class InputModel
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

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Surname")]
            public string Surname { get; set; }

            [Required]
            [Display(Name = "JMBAG")]
            public string JMBAG { get; set; }

            [Required]
            [Display(Name="Role")]
            public string Role { get; set; }


        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (RoleList == null) {
                RoleList = new List<SelectListItem>();
            }

            RoleList.Add(new SelectListItem(StaticDetails.ProfessorRole, StaticDetails.ProfessorRole));
            RoleList.Add(new SelectListItem(StaticDetails.StudentRole, StaticDetails.StudentRole));
            //RoleList.Add(new SelectListItem(StaticDetails.AdminRole, StaticDetails.AdminRole));
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new Korisnik
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name,
                    Surname = Input.Surname,
                    JMBAG = Input.JMBAG
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
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

                    var userCheck = await _userManager.Users.ToListAsync();

                    if (userCheck.Count == 1) {
                        Input.Role = StaticDetails.AdminRole;
                    }

                    await _userManager.AddToRoleAsync(user, Input.Role);

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
