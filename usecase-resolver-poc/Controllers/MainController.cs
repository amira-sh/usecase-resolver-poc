using Microsoft.AspNetCore.Mvc;
using Domain.Models.UsecaseA;
using Domain.Models.UsecaseB;
using UCMediator.Contracts;

namespace usecase_resolver_poc.Controllers
{
    [Route("")]
    [ApiController]
    public class MainController(IUCMediator mediator) : ControllerBase
    {
        [HttpGet("UsecaseA")]
        public async Task<IActionResult> GetA()
        {
            var usecaseAResponse = await mediator.ExecuteAsync<UsecaseAResponse>(new UsecaseARequest());
            return Ok(usecaseAResponse.Result);
        }

        [HttpGet("UsecaseB")]
        public async Task<IActionResult> GetB()
        {
            var usecaseAResponse = await mediator.ExecuteAsync<UsecaseBResponse>(new UsecaseBRequest());
            return Ok(usecaseAResponse.Result);
        }
    }
}
