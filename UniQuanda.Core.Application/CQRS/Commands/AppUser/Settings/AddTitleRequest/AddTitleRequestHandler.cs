using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Settings.AddTitleRequest
{
    public class AddTitleRequestHandler : IRequestHandler<AddTitleRequestCommand, bool>
    {
        private readonly IAcademicTitleRepository _academicTitleRepository;
        private readonly IImageService _imageService;
        public AddTitleRequestHandler(IAcademicTitleRepository academicTitleRepository, IImageService imageService)
        {
            _academicTitleRepository = academicTitleRepository;
            _imageService = imageService;
        }
        public async Task<bool> Handle(AddTitleRequestCommand request, CancellationToken ct)
        {
            var requestId = await _academicTitleRepository.GetNextTitleRequestIdAsync(ct);
            var imageEndpoint = _imageService.GetImageURL();
            var guid = Guid.NewGuid().ToString();
            var imageName = $"{request.UserId}/{requestId}/scan-{guid}";
            var imageSaveResult = await _imageService.SaveImageAsync(request.Scan, imageName, ImageFolder.TitleRequest, ct);
            if (!imageSaveResult) return false;
            imageName = $"{imageEndpoint}{ImageFolder.TitleRequest.Value}/{imageName}";
            return await _academicTitleRepository.AddAcademicTitleRequestForUserAsync(
                requestId, request.UserId,
                imageName, request.AcademicTitleId,
                request.AdditionalInfo, request.CreatedAt,
                ct
                );
        }
    }
}
