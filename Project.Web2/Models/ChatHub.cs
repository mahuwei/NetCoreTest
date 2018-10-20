using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Project.Web2.Models {
    public class ChatHub : Hub {

        public ChatHub() {
            Console.WriteLine("ChatHub ctor.");
        }
        public async Task SendMessage(string user, string message) {
            Console.WriteLine($"{Context.ConnectionId} {Context.User}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}