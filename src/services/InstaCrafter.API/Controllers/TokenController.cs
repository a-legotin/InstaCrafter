using System.Threading.Tasks;
using InstaCrafter.API.Commands;
using InstaCrafter.Infrastructure.Identity.Models.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<TokenResponse> AuthenticateAsync([FromBody] AuthenticateCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Resource;
        }
    }
}