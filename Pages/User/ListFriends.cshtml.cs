using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using termProject_201811010.Models;
using System.Security.Claims;

namespace termProject_201811010.Pages.User
{

    public class ListFriendsModel : PageModel
    {
        private readonly termProject_201811010.Models.canbookramContext _context;

        public List<Friendship> friendsList { get; set; }
        public ListFriendsModel(canbookramContext context)
        {
            _context = context;
        }
        public List<UserProfile> users { get; set; }

        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            friendsList = _context.Friendships.Where(f => ( f.ResevierId == userId) && (f.Approval == true)).ToList();
            users = _context.UserProfiles.ToList();


        }
        public IActionResult OnPostDeleteFriend( string deleteId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var friendShip = _context.Friendships.FirstOrDefault(f => f.SenderId == deleteId && userId == f.ResevierId);

            _context.Friendships.Remove(friendShip);
            _context.SaveChanges();

            return RedirectToPage("/User/ListFriends");
        }
    }
}
