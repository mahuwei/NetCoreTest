using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Domain;

namespace Project.Web2.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase {
        private readonly IMediator _mediator;

        public BusinessController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Business business) {
            var response = await _mediator.Send(business);

            if (response.Errors.Any()) return BadRequest(response.Errors);

            return Ok(response.Result);
        }
    }
}