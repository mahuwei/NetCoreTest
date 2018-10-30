using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Web2.Models.MediatRTest;

namespace Project.Web2.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public ActionResult Register(NewUser user) {
            var task = _mediator.Publish(user);
            return Ok(user);
        }


        [HttpPost]
        [Route("PostMessage")]
        public async Task<Pong> PostMessage(string message) {
            var ping = new Ping {
                Message = message
            };
            var task = await _mediator.Send(ping);
            var ret = task.Message;
            return task;
        }
    }
}