using MediatR;
using UniQuanda.Core.Application.Services;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class AddImageHandler : IRequestHandler<AddImageCommand, AddImageResponseDTO>
{
    private readonly IImageService _imageService;

    public AddImageHandler(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task<AddImageResponseDTO> Handle(AddImageCommand request, CancellationToken ct)
    {
        var result = await _imageService.SaveImageAsync(request.Image, request.ImageName, request.Folder, ct);
        return new()
        {
            IsSuccess = result
        };
    }
}