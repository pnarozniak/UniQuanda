using MediatR;
using UniQuanda.Core.Application.Services;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class RemoveImageHandler : IRequestHandler<RemoveImageCommand, RemoveImageResponseDTO>
{
    private readonly IImageService _imageService;

    public RemoveImageHandler(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task<RemoveImageResponseDTO> Handle(RemoveImageCommand request, CancellationToken ct)
    {
        var result = await _imageService.RemoveImageAsync(request.ImageName, request.Folder, ct);
        return new()
        {
            IsSuccess = result
        };
    }
}