using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using termProject_201811010.Models;
using System.Security.Claims;
namespace termProject_201811010.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly termProject_201811010.Models.canbookramContext _context;

        public IndexModel(canbookramContext context)
        {
            _context = context;
        }
        public  List<UserProfile> userProfiles {  get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public void OnGet()
        {
            
            if (!string.IsNullOrEmpty(SearchString))
            {
                userProfiles = _context.UserProfiles.Where(u => u.FirstName.Contains(SearchString)).ToList();  
            }
            else
            {
                userProfiles = _context.UserProfiles.ToList();
            }
        }

       
        public IActionResult OnPostFriendRequest(string Id)
        {
            var friendRequest = new Friendship()
            {
                Approval = false,
                SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                ResevierId = Id
            };
            _context.Friendships.Add(friendRequest);
            _context.SaveChanges();

            return RedirectToPage("/User/Index");
        }
       
        
    }
}
