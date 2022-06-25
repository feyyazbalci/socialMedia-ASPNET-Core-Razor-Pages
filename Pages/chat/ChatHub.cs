using Microsoft.AspNetCore.SignalR;
using termProject_201811010.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace termProject_201811010.Pages.chat
{
    public class ChatHub : Hub
    {
        private readonly canbookramContext _context;

        public ChatHub(canbookramContext context)
        {
            _context = context;
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(string user, string message)
        {
            //message send to all users
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task<Task> SendMessageToGroup(string sender, string receiver, string message)
        {
            //message send to receiver only
            var messageReceived = new Message() { MessageTime = DateTime.Now, ReceiverId = receiver, SenderId = sender, Text = message };
            await _context.Messages.AddAsync(messageReceived);
            await _context.SaveChangesAsync() ;
            
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
            
        }
    }
}
