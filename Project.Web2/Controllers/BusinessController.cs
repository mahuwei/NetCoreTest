using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Domain;

namespace Project.Web2.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BusinessController : ControllerBase {
        private readonly IMediator _mediator;
        private readonly ILogger<BusinessController> _logger;

        public BusinessController(IMediator mediator,
            ILogger<BusinessController> logger) {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Business business) {
            var response = await _mediator.Send(business);

            if (response.Errors.Any()) return BadRequest(response.Errors);

            return Ok(response.Result);
        }

        [HttpPost]
        public IActionResult LogTest([FromBody] TempData data) {
            _logger.LogInformation("LogInformation,data:{@data}", data);
            try {
                throw new Exception("test error.");
            }
            catch (Exception e) {
                _logger.LogError(e, "LogError,data:{@data}", data);
            }

            _logger.LogWarning("LogWarning,data:{@data}", data);
            _logger.LogDebug("LogDebug,Data:{@data}", data);
            return Ok(data);
        }
    }

    public class TempData {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}