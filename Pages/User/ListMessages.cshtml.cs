using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using termProject_201811010.Models;
using System.Security.Claims; 

namespace termProject_201811010.Pages.User
{
    public class ListMessagesModel : PageModel
    {
        private readonly canbookramContext _context;
        public List<Message>UserMessage { get; set; }

        public List<UserProfile> users { get; set; }
        public ListMessagesModel(canbookramContext context)
        {
            _context = context;
            
        }

        public void OnGet()
        {
            var userMail = User.FindFirstValue(ClaimTypes.Name);
            UserMessage = _context.Messages.Where(m => m.ReceiverId == userMail).ToList();
            users = _context.UserProfiles.ToList();
        }
    }
}
