using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using termProject_201811010.Models;


namespace termProject_201811010.Pages.Post
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly termProject_201811010.Models.canbookramContext _context;

        public IndexModel(termProject_201811010.Models.canbookramContext context)
        {
            _context = context;
        }

        public List<UserProfile> userProfiles { get; set; }
        public IList<UserPost> UserPost { get; set; } = default!;

        public async Task OnGetAsync()
        {
            userProfiles = _context.UserProfiles.ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            //UserPost = await _context.UserPosts.Where(u => u.UserId == userId).ToListAsync();
            UserPost = await _context.UserPosts.ToListAsync();


        }
        public IActionResult OnPostCalculateLikes(int Id)
        {
            var post = _context.UserPosts.FirstOrDefault(pp => pp.Id == Id);

            post.Likes++;

            _context.SaveChanges();
            return RedirectToPage("/Post/Index");
        }
        public IActionResult OnPostCalculateDislikes(int Id)
        {
            var post = _context.UserPosts.FirstOrDefault(pp => pp.Id == Id);

            post.Dislikes++;

            _context.SaveChanges();
            return RedirectToPage("/Post/Index");
        }
    }
}
