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
    public class DetailsModel : PageModel
    {
        private readonly termProject_201811010.Models.canbookramContext _context;

        public DetailsModel(termProject_201811010.Models.canbookramContext context)
        {
            _context = context;
        }

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
    }
}
