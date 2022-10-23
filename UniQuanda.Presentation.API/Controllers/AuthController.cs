using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Login;
using UniQuanda.Core.Application.CQRS.Commands.Auth.RecoverPassword;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Register;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ResendRegisterConfirmationCode;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ResetPasword;
using UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailAndNicknameAvailable;

namespace UniQuanda.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Checks if given e-mail address and given nickname are already used by any users
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IsEmailAndNicknameAvailableResponseDTO))]
    [HttpGet("is-email-and-nickname-available")]
    public async Task<IActionResult> IsEmailAndNicknameAvailable(
        [FromQuery] IsEmailAndNicknameAvailableRequestDTO request,
        CancellationToken ct)
    {
        var query = new IsEmailAndNicknameAvailableQuery(request);
        var isEmailAndNicknameAvailable = await _mediator.Send(query, ct);
        return Ok(isEmailAndNicknameAvailable);
    }

    /// <summary>
    ///     Registers new user in database and sends confirmation code to user e-mail address
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestDTO request,
        CancellationToken ct)
    {
        var command = new RegisterCommand(request);
        var isRegistered = await _mediator.Send(command, ct);
        return isRegistered ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }

    /// <summary>
    ///     Performs login operation and returns JWT access token and refresh token
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDTO request,
        CancellationToken ct)
    {
        var command = new LoginCommand(request);
        var loginResult = await _mediator.Send(command, ct);
        return loginResult.Status switch
        {
            LoginResponseDTO.LoginStatus.InvalidCredentials => NotFound(),
            _ => Ok(loginResult)
        };
    }

    /// <summary>
    ///     Confirms user registration process
    /// </summary>
    [HttpPost("confirm-register")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmRegister(
        [FromBody] ConfirmRegisterRequestDTO request,
        CancellationToken ct)
    {
        var command = new ConfirmRegisterCommand(request);
        var isConfirmed = await _mediator.Send(command, ct);
        return isConfirmed ? NoContent() : NotFound();
    }

    /// <summary>
    ///     Resets and re-sends e-mail confirmation code for given user
    /// </summary>
    [HttpPost("resend-register-confirmation-code")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ResendRegisterConfirmationCode(
        [FromBody] ResendRegisterConfirmationCodeRequestDTO request,
        CancellationToken ct)
    {
        var command = new ResendRegisterConfirmationCodeCommand(request);
        await _mediator.Send(command, ct);
        return NoContent();
    }

    /// <summary>
    ///     Generates password recovery link and sends it via e-mail
    /// </summary>
    [HttpPost("recover-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecoverPassword(
        [FromBody] RecoverPasswordDTO request,
        CancellationToken ct)
    {
        var command = new RecoverPasswordCommand(request);
        await _mediator.Send(command, ct);
        return NoContent();
    }

    /// <summary>
    ///     Resets user password
    /// </summary>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPaswordDTO request,
        CancellationToken ct)
    {
        var command = new ResetPasswordCommand(request);
        var success = await _mediator.Send(command, ct);
        return success ? NoContent() : Conflict();
    }
}