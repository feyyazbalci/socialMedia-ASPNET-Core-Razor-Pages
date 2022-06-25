using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using termProject_201811010.Models;

namespace termProject_201811010.Pages.Post
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly termProject_201811010.Models.canbookramContext _context;
        private readonly UserManager<IdentityUser> _userManager;



        public CreateModel(termProject_201811010.Models.canbookramContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
           
            return Page();
        }

        [BindProperty]
        public UserPost UserPost { get; set; } = default!;
        [BindProperty]
        public MessagePictureFile? FileUpload { get; set; }

       


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.UserPosts == null || UserPost == null)
            {
                return Page();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            UserPost.UserId = userId;

            if (FileUpload.FormFile != null)
            {
                var memoryStream = new MemoryStream();
                await FileUpload.FormFile.CopyToAsync(memoryStream);
                UserPost.Picture = memoryStream.ToArray();
            }
            else { UserPost.Picture = null; }

            UserPost.Timestamp = DateTime.Now;

            _context.UserPosts.Add(UserPost);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
