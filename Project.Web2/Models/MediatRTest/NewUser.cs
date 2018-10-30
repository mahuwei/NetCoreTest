using MediatR;

namespace Project.Web2.Models.MediatRTest {
    public class NewUser : INotification {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}