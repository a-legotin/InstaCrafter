using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.API.Classes;
using InstaCrafter.API.CommandResponses;
using InstaCrafter.API.Commands;
using InstaCrafter.Infrastructure.Identity.Models.Authentication;
using InstaCrafter.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InstaCrafter.API.Handlers
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, CommandResponse>
    {
        private readonly HttpContext? _httpContext;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        
        public AuthenticateCommandHandler(ITokenService tokenService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContext = (httpContextAccessor != null)
                ? httpContextAccessor.HttpContext
                : throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        
        public async Task<CommandResponse> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
        {
            CommandResponse response = new();

            var ipAddress = _httpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString();

            TokenResponse tokenResponse = await _tokenService.Authenticate(command, ipAddress ?? string.Empty);
            if (tokenResponse == null)
            {
                throw new InvalidCredentialsException();
            }

            response.Resource = tokenResponse;
            return response;
        }
    }
}