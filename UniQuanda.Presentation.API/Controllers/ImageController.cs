using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ImageController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Gets image by url
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(File))]
    [HttpGet("{FolderName}/{FileName}")]
    public async Task<IActionResult> GetImage(
        [FromRoute] string FolderName,
        [FromRoute] string FileName,
        CancellationToken ct)
    {

        // TODO: Authorization for the resource based on User
        var dto = new GetImageRequestDTO
        {
            Folder = ImageFolder.FindByValue(FolderName),
            FileName = FileName
        };
        var query = new GetImageQuery(dto);
        var result = await _mediator.Send(query, ct);
        if (object.Equals(result, default((Stream DataStream, string ContentType)))) return NotFound();
        return File(result.File, result.ContentType);
    }

    /// <summary>
    ///     Saves image by url
    /// </summary>
    [HttpPost("{FolderName}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddImage(
        [FromRoute] string FolderName,
        [FromBody] AddImageRequestDTO file,
        CancellationToken ct)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development") return StatusCode(StatusCodes.Status423Locked);

        var command = new AddImageCommand(file, ImageFolder.FindByValue(FolderName));
        var result = await _mediator.Send(command, ct);
        return result.IsSuccess ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }

    /// <summary>
    ///     Removes image by url
    /// </summary>
    [HttpDelete("{FolderName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveImage(
        [FromRoute] string FolderName,
        [FromBody] RemoveImageRequestDTO fileData,
        CancellationToken ct)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development") return StatusCode(StatusCodes.Status423Locked);
        var command = new RemoveImageCommand(fileData, ImageFolder.FindByValue(FolderName));
        var result = await _mediator.Send(command, ct);
        return result.IsSuccess ? StatusCode(StatusCodes.Status204NoContent) : NotFound();
    }

}