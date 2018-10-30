using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Project.Web2.Models.MediatRTest {
    public class EmailHandler : INotificationHandler<NewUser> {
        public Task Handle(NewUser notification,
            CancellationToken cancellationToken) {
            //Send email  
            Debug.WriteLine(" ****  Email sent to user  *****");
            return Task.FromResult(true);
        }
    }

    public class LogHandlerLogHandler : INotificationHandler<NewUser> {
        public Task Handle(NewUser notification,
            CancellationToken cancellationToken) {
            //Save to log  
            Debug.WriteLine(" ****  User save to log  *****");
            return Task.FromResult(true);
        }
    }


    public class NewUserHandler : INotificationHandler<NewUser> {
        public Task Handle(NewUser notification,
            CancellationToken cancellationToken) {
            //Save to log  
            Debug.WriteLine(" ****  Save user in database  *****");
            return Task.FromResult(notification);
        }
    }


}