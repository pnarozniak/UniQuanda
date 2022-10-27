using MediatR;
using UniQuanda.Core.Application.Services;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class GetImageHandler : IRequestHandler<GetImageQuery, GetImageResponseDTO>
{
    private readonly IImageService _imageService;

    public GetImageHandler(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task<GetImageResponseDTO> Handle(GetImageQuery request, CancellationToken ct)
    {
        var result = await _imageService.GetImageAsync(request.ImageName, request.Folder, ct);
        return new()
        {
            Image = result.DataStream,
            ContentType = result.ContentType
        };
    }
}