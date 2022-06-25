using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using termProject_201811010.Models;
using System.Security.Claims;

namespace termProject_201811010.Pages.User
{
    public class FriendRequestModel : PageModel
    {
        private readonly termProject_201811010.Models.canbookramContext _context;

        public List<Friendship> friendships { get; set; }  

        public List<UserProfile> users { get; set; }
        public FriendRequestModel(canbookramContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            friendships = _context.Friendships.Where(f=> f.ResevierId == userID && f.Approval == false).ToList();
            users = _context.UserProfiles.ToList();


        }
        public IActionResult OnPostAcceptFriend (bool accept, int FriendshipId)
        {
            var friendShip = _context.Friendships.FirstOrDefault(f => f.Id == FriendshipId);
            if (accept)
            {
                friendShip.Approval = true;
            }
            else
            {
                _context.Friendships.Remove(friendShip);
                
            }
            _context.SaveChanges();
            return RedirectToPage("/user/ListRequest");
        }
    }
}
