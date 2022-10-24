using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmailForUser;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister;
using UniQuanda.Core.Application.CQRS.Commands.Auth.DeleteExtraEmail;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Login;
using UniQuanda.Core.Application.CQRS.Commands.Auth.Register;
using UniQuanda.Core.Application.CQRS.Commands.Auth.ResendRegisterConfirmationCode;
using UniQuanda.Core.Application.CQRS.Commands.Auth.UpdatePassword;
using UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateUserMainEmail;
using UniQuanda.Core.Application.CQRS.Queries.Auth.GetUserEmails;
using UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailAndNicknameAvailable;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Enums;
using UniQuanda.Presentation.API.Extensions;
using UniQuanda.Presentation.API.Utils;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IsEmailAndNicknameAvailableResponseDTO))]
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
    ///     Confirms user registration process
    /// </summary>
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
    ///     Get all emails connected with User
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserEmailsReponseDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("get-user-emails")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetUserEmails(CancellationToken ct)
    {
        var command = new GetUserEmailsQuery(User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>
    ///     Update main email assinged to user
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(AuthConflictResponseDTO))]
    [HttpPut("update-main-email")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> UpdateUserMainEmail([FromBody] UpdateUserMainEmailRequestDTO request, CancellationToken ct)
    {
        var command = new UpdateUserMainEmailCommand(request, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result switch
        {
            UpdateResultOfEmailOrPasswordEnum.ContentNotExist => NotFound(),
            UpdateResultOfEmailOrPasswordEnum.InvalidPassword => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.InvalidPassword }),
            UpdateResultOfEmailOrPasswordEnum.EmailNotAvailable => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.EmailNotAvailable }),
            UpdateResultOfEmailOrPasswordEnum.NotSuccessful => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.DbConflict }),
            UpdateResultOfEmailOrPasswordEnum.Successful => NoContent()
        };
    }

    /// <summary>
    ///     Add extra email to user
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(AuthConflictResponseDTO))]
    [HttpPost("add-extra-email")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> AddExtraEmail([FromBody] AddExtraEmailForUserRequestDTO request, CancellationToken ct)
    {
        var command = new AddExtraEmailForUserCommand(request, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result switch
        {
            UpdateResultOfEmailOrPasswordEnum.ContentNotExist => NotFound(),
            UpdateResultOfEmailOrPasswordEnum.EmailNotAvailable => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.EmailNotAvailable }),
            UpdateResultOfEmailOrPasswordEnum.OverLimitOfExtraEmails => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.OverLimitOfExtraEmails }),
            UpdateResultOfEmailOrPasswordEnum.InvalidPassword => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.InvalidPassword }),
            UpdateResultOfEmailOrPasswordEnum.NotSuccessful => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.DbConflict }),
            UpdateResultOfEmailOrPasswordEnum.Successful => NoContent()
        };
    }

    /// <summary>
    ///     Updater user password
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(AuthConflictResponseDTO))]
    [HttpPut("update-user-passsword")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UpdatePasswordRequestDTO request, CancellationToken ct)
    {
        var command = new UpdatePasswordCommand(request, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result switch
        {
            UpdateResultOfEmailOrPasswordEnum.ContentNotExist => NotFound(),
            UpdateResultOfEmailOrPasswordEnum.InvalidPassword => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.InvalidPassword }),
            UpdateResultOfEmailOrPasswordEnum.NotSuccessful => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.DbConflict }),
            UpdateResultOfEmailOrPasswordEnum.Successful => NoContent()
        };
    }

    /// <summary>
    ///     Delete extra email of user
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(AuthConflictResponseDTO))]
    [HttpDelete("delete-extra-email")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> DeleteExtraEmail([FromBody] DeleteExtraEmailRequestDTO request, CancellationToken ct)
    {
        var command = new DeleteExtraEmailCommand(request, User.GetId()!.Value);
        var result = await _mediator.Send(command, ct);
        return result switch
        {
            UpdateResultOfEmailOrPasswordEnum.ContentNotExist => NotFound(),
            UpdateResultOfEmailOrPasswordEnum.InvalidPassword => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.InvalidPassword }),
            UpdateResultOfEmailOrPasswordEnum.NotSuccessful => Conflict(new AuthConflictResponseDTO { Status = ConflictResponseStatus.DbConflict }),
            UpdateResultOfEmailOrPasswordEnum.Successful => NoContent()
        };
    }
}