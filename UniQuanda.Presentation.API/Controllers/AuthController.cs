using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmail;
using UniQuanda.Core.Application.CQRS.Commands.Auth.CancelEmailConfirmation;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmEmail;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmOAuthRegister;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister;
using UniQuanda.Core.Application.CQRS.Commands.Auth.DeleteExtraEmail;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Login;
using UniQuanda.Core.Application.CQRS.Commands.Auth.LoginByGoogle;
using UniQuanda.Core.Application.CQRS.Commands.Auth.RecoverPassword;
using UniQuanda.Core.Application.CQRS.Commands.Auth.RefreshToken;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Register;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ResendEmailWithConfirmationEmailLink;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ResendRegisterConfirmationCode;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ResetPasword;
using UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateMainEmail;
using UniQuanda.Core.Application.CQRS.Commands.Auth.UpdatePassword;
using UniQuanda.Core.Application.CQRS.Queries.Auth.GetUserEmails;
using UniQuanda.Core.Application.CQRS.Queries.Auth.GetUserInfo;
using UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailAndNicknameAvailable;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Infrastructure.Enums;
using UniQuanda.Presentation.API.Attributes;
using UniQuanda.Presentation.API.Extensions;

namespace UniQuanda.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    [Recaptcha]
    [HttpGet("is-email-and-nickname-available")]
    [AllowAnonymous]
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
    [Recaptcha]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [AllowAnonymous]
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
    [Recaptcha]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
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
    ///     Performs login or register by google provider
    /// </summary>
    [HttpGet("login-by-google")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status307TemporaryRedirect)]
    public async Task<IActionResult> LoginByGoogle(
        [FromQuery] LoginByGoogleRequestDTO request,
        CancellationToken ct)
    {
        var command = new LoginByGoogleCommand(request);
        var redirectResultUrl = await _mediator.Send(command, ct);
        return new RedirectResult(redirectResultUrl, false);
    }

    /// <summary>
    ///     Performs login or register by google provider
    /// </summary>
    [Recaptcha]
    [HttpPost("confirm-oauth-register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConfirmOAuthRegisterResponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmOAuthRegister(
        [FromBody] ConfirmOAuthRegisterRequestDTO request,
        CancellationToken ct)
    {
        var command = new ConfirmOAuthRegisterCommand(request);
        var response = await _mediator.Send(command, ct);
        return response is null ? NotFound() : Ok(response);
    }

    /// <summary>
    ///     Gets user info based on access token
    /// </summary>
    [Recaptcha]
    [HttpGet("user-info")]
    [Authorize(Roles = JwtTokenRole.User)]
    [Authorize(Roles = JwtTokenRole.OAuthAccount)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UserInfo(CancellationToken ct)
    {
        var query = new GetUserInfoQuery(User.GetId()!.Value);
        var userInfo = await _mediator.Send(query, ct);
        return userInfo is null ? NotFound() : Ok(userInfo);
    }

    /// <summary>
    ///     Confirms user registration process
    /// </summary>
    [Recaptcha]
    [HttpPost("confirm-register")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
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
    [Recaptcha]
    [HttpPost("resend-register-confirmation-code")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AllowAnonymous]
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
    [Recaptcha]
    [HttpPost("recover-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AllowAnonymous]
    public async Task<IActionResult> RecoverPassword(
        [FromBody] RecoverPasswordDTO request,
        CancellationToken ct)
    {
        var command = new RecoverPasswordCommand(request, HttpContext.GetUserAgentInfo());
        await _mediator.Send(command, ct);
        return NoContent();
    }

    /// <summary>
    ///     Resets user password
    /// </summary>
    [Recaptcha]
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPaswordDTO request,
        CancellationToken ct)
    {
        var command = new ResetPasswordCommand(request, HttpContext.GetUserAgentInfo());
        var success = await _mediator.Send(command, ct);
        return success ? NoContent() : Conflict();
    }

    /// <summary>
    ///     Refreshes access and refresh tokens
    /// </summary>
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshTokenResponseDTO))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequestDTO request,
        CancellationToken ct)
    {
        var command = new RefreshTokenCommand(request);
        var tokens = await _mediator.Send(command, ct);
        return tokens is not null ? Ok(tokens) : Conflict();
    }

    /// <summary>
    ///     Get all emails connected with User
    /// </summary>
    [Recaptcha]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserEmailsReponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-user-emails")]
    [Authorize(Roles = JwtTokenRole.User)]
    [Authorize(Roles = JwtTokenRole.UniquandaAccount)]
    public async Task<IActionResult> GetUserEmails(CancellationToken ct)
    {
        var command = new GetUserEmailsQuery(User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    ///     Update main email assinged to user
    /// </summary>
    [Recaptcha]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(UpdateMainEmailResponseDTO))]
    [HttpPut("update-main-email")]
    [Authorize(Roles = JwtTokenRole.User)]
    [Authorize(Roles = JwtTokenRole.UniquandaAccount)]
    public async Task<IActionResult> UpdateUserMainEmail([FromBody] UpdateMainEmailRequestDTO request, CancellationToken ct)
    {
        var command = new UpdateMainEmailCommand(request, User.GetId()!.Value, HttpContext.GetUserAgentInfo());
        var result = await _mediator.Send(command, ct);
        return result.ActionResult switch
        {
            AppUserSecurityActionResultEnum.Successful => NoContent(),
            AppUserSecurityActionResultEnum.ContentNotExist => NotFound(),
            _ => Conflict(result)
        };
    }

    /// <summary>
    ///     Add extra email to user
    /// </summary>
    [Recaptcha]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(AddExtraEmailResponseDTO))]
    [HttpPost("add-extra-email")]
    [Authorize(Roles = JwtTokenRole.User)]
    [Authorize(Roles = JwtTokenRole.UniquandaAccount)]
    public async Task<IActionResult> AddExtraEmail([FromBody] AddExtraEmailRequestDTO request, CancellationToken ct)
    {
        var command = new AddExtraEmailCommand(request, User.GetId()!.Value, HttpContext.GetUserAgentInfo());
        var result = await _mediator.Send(command, ct);
        return result.ActionResult switch
        {
            AppUserSecurityActionResultEnum.Successful => NoContent(),
            AppUserSecurityActionResultEnum.ContentNotExist => NotFound(),
            _ => Conflict(result)
        };
    }

    /// <summary>
    ///     Updater user password
    /// </summary>
    [Recaptcha]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(UpdatePasswordResponseDTO))]
    [HttpPut("update-user-password")]
    [Authorize(Roles = JwtTokenRole.User)]
    [Authorize(Roles = JwtTokenRole.UniquandaAccount)]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UpdatePasswordRequestDTO request, CancellationToken ct)
    {
        var command = new UpdatePasswordCommand(request, User.GetId()!.Value, HttpContext.GetUserAgentInfo());
        var result = await _mediator.Send(command, ct);
        return result.ActionResult switch
        {
            AppUserSecurityActionResultEnum.Successful => NoContent(),
            AppUserSecurityActionResultEnum.ContentNotExist => NotFound(),
            _ => Conflict(result)
        };
    }

    /// <summary>
    ///     Delete extra email of user
    /// </summary>
    [Recaptcha]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(DeleteExtraEmailResponseDTO))]
    [HttpPost("delete-extra-email")]
    [Authorize(Roles = JwtTokenRole.User)]
    [Authorize(Roles = JwtTokenRole.UniquandaAccount)]
    public async Task<IActionResult> DeleteExtraEmail([FromBody] DeleteExtraEmailRequestDTO request, CancellationToken ct)
    {
        var command = new DeleteExtraEmailCommand(request, User.GetId()!.Value, HttpContext.GetUserAgentInfo());
        var result = await _mediator.Send(command, ct);
        return result.ActionResult switch
        {
            AppUserSecurityActionResultEnum.ContentNotExist => NotFound(),
            AppUserSecurityActionResultEnum.Successful => NoContent(),
            _ => Conflict(result)
        };
    }

    /// <summary>
    ///     Confirm user email
    /// </summary>
    [Recaptcha]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost("confirm-email")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmUserEmail([FromBody] ConfirmEmailRequestDTO request, CancellationToken ct)
    {
        var command = new ConfirmEmailCommand(request, HttpContext.GetUserAgentInfo());
        var result = await _mediator.Send(command, ct);
        return result ? NoContent() : Conflict();
    }

    /// <summary>
    ///     Confirm user email
    /// </summary>
    [Recaptcha]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost("resend-confirmation-email")]
    [Authorize(Roles = JwtTokenRole.User)]
    [Authorize(Roles = JwtTokenRole.UniquandaAccount)]
    public async Task<IActionResult> ResendConfirmationEmail(CancellationToken ct)
    {
        var command = new ResendEmailWithConfirmationEmailLinkCommand(User.GetId()!.Value, HttpContext.GetUserAgentInfo());
        var result = await _mediator.Send(command, ct);
        return result ? NoContent() : Conflict();
    }

    /// <summary>
    ///     Cancel user email to confirm
    /// </summary>
    [Recaptcha]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpDelete("cancel-email-confirmation")]
    [Authorize(Roles = JwtTokenRole.User)]
    [Authorize(Roles = JwtTokenRole.UniquandaAccount)]
    public async Task<IActionResult> CancelEmailConfirmation(CancellationToken ct)
    {
        var command = new CancelEmailConfirmationCommand(User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result ? NoContent() : Conflict();
    }
}