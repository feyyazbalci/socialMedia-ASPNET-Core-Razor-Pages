// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using termProject_201811010.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;



namespace termProject_201811010.Areas.Identity.Pages.Account.Manage
{
    public class PictureModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly canbookramContext _Context;

        public PictureModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, canbookramContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Context = context;
        }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 

        //yeni eklendi
        [BindProperty]
        public BufferedSingleFileUploadDb? FileUpload { get; set; }

        public byte[] Picture { get; set; }

        public UserProfile UserDetails { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }




        [TempData]
        public string StatusMessage { get; set; }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


        }




        private async Task LoadAsync(IdentityUser user)
        {
            var UserDetails = _Context.UserProfiles.Where(p => p.UserId == user.Id).FirstOrDefault();
            if (UserDetails != null)
            {
                Picture = UserDetails.Picture;
            }
            else
            {
                //Read Image File into Image object.
                string path = "./wwwroot/images/empty_Profile.png";
                var memoryStream = new MemoryStream();
                using (var stream = System.IO.File.OpenRead(path))
                {
                    await new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name)).CopyToAsync(memoryStream);
                    Picture = memoryStream.ToArray();
                };
            }
        }
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return;
            }

            LoadAsync(user);
        }



        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {



            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            UserDetails = _Context.UserProfiles.Where(p => p.UserId == user.Id).FirstOrDefault();
            if (UserDetails.Picture != Picture)
            {
                var memoryStream = new MemoryStream();
                await FileUpload.FormFile.CopyToAsync(memoryStream);
                UserDetails.Picture = memoryStream.ToArray();
                UserDetails.UserId = user.Id;

                _Context.UserProfiles.Update(UserDetails);
                await _Context.SaveChangesAsync();
                StatusMessage = "Your profile photo is changed.";

                return RedirectToPage();
            }
            return RedirectToPage();
        }



        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }


    }
}
