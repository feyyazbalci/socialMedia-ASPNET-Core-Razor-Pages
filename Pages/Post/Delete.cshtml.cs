using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using termProject_201811010.Models;

namespace termProject_201811010.Pages.Post
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly termProject_201811010.Models.canbookramContext _context;

        public DeleteModel(termProject_201811010.Models.canbookramContext context)
        {
            _context = context;
        }

        [BindProperty]
      public UserPost UserPost { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserPosts == null)
            {
                return NotFound();
            }

            var userpost = await _context.UserPosts.FirstOrDefaultAsync(m => m.Id == id);

            if (userpost == null)
            {
                return NotFound();
            }
            else 
            {
                UserPost = userpost;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UserPosts == null)
            {
                return NotFound();
            }
            var userpost = await _context.UserPosts.FindAsync(id);

            if (userpost != null)
            {
                UserPost = userpost;
                _context.UserPosts.Remove(UserPost);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
