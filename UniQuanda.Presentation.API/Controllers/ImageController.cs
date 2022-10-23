using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
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
        var result = await _imageService.GetImageAsync(FileName, ImageFolder.FindByValue(FolderName), ct);
        if (object.Equals(result, default((Stream DataStream, string ContentType)))) return NotFound();
        return File(result.DataStream, result.ContentType);
    }

    /// <summary>
    ///     Saves image by url
    /// </summary>
    [HttpPost("{FolderName}/{FileName}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddImage(
        [FromRoute] string FolderName,
        [FromRoute] string FileName,
        IFormFile file,
        CancellationToken ct)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development") return StatusCode(StatusCodes.Status423Locked);

        var result = await _imageService.SaveImageAsync(file, FileName, ImageFolder.FindByValue(FolderName), ct);
        return result ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }

    /// <summary>
    ///     Removes image by url
    /// </summary>
    [HttpDelete("{FolderName}/{FileName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteImage(
        [FromRoute] string FolderName,
        [FromRoute] string FileName,
        CancellationToken ct)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development") return StatusCode(StatusCodes.Status423Locked);

        var result = await _imageService.RemoveImageAsync(FileName, ImageFolder.FindByValue(FolderName), ct);
        return result ? StatusCode(StatusCodes.Status204NoContent) : NotFound();
    }

}