using System.Threading.Tasks;
using InstaCrafter.API.Commands;
using InstaCrafter.Infrastructure.Identity.Models.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.API.Controllers
{
    /// <summary>
    ///     All token related actions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController
    {
        private readonly IMediator _mediator;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="mediator"></param>
        public TokenController(IMediator mediator) => _mediator = mediator;

        // POST: api/Token/Authenticate
        /// <summary>
        ///     Validate that the user account is valid and return an auth token
        ///     to the requesting app for use in the api.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
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