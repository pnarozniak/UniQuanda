using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Login;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Register;
using UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailFree;
using UniQuanda.Core.Application.CQRS.Queries.Auth.IsNicknameFree;

namespace UniQuanda.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Checks if given e-mail address is already used by any user
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [HttpPost("is-email-free")]
        public async Task<IActionResult> IsEmailFree([FromBody] IsEmailFreeRequestDTO request)
        {
            var query = new IsEmailFreeQuery(request);
            var isEmailFree = await _mediator.Send(query);
            return Ok(isEmailFree);
        }

        /// <summary>
        /// Checks if given nickname is already used by any user
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [HttpPost("is-nickname-free")]
        public async Task<IActionResult> IsNicknameFree([FromBody] IsNicknameFreeRequestDTO request)
        {
            var query = new IsNicknameFreeQuery(request);
            var isNicknameFree = await _mediator.Send(query);
            return Ok(isNicknameFree);
        }

        /// <summary>
        /// Registers new user in database and sends confirmation code to user e-mail address
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            var command = new RegisterCommand(request);
            var isRegistered = await _mediator.Send(command);
            return isRegistered ? StatusCode(StatusCodes.Status201Created) : Conflict();
        }

        /// <summary>
        /// Performs login operation and returns JWT access token and refresh token
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDTO))]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var command = new LoginCommand(request);
            var loginResult = await _mediator.Send(command);
            return loginResult.Status switch
            {
                LoginResponseDTO.LoginStatus.InvalidCredentials => NotFound(),
                _ => Ok(loginResult)
            };
        }
    }
}