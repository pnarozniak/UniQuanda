using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.AddAnswer;

public class AddAnswerHandler : IRequestHandler<AddAnswerCommand, bool>
{
    private readonly IImageService _imageService;
    private readonly IHtmlService _htmlService;
    private readonly IContentRepository _contentRepository;
    private readonly IAnswerRepository _answerRepository;

    public AddAnswerHandler(
        IImageService imageService,
        IHtmlService htmlService,
        IContentRepository contentRepository,
        IAnswerRepository answerRepository
    )
    {
        _imageService = imageService;
        _htmlService = htmlService;
        _contentRepository = contentRepository;
        _answerRepository = answerRepository;
    }

    public async Task<bool> Handle(AddAnswerCommand request, CancellationToken ct)
    {
        var contentId = await _contentRepository.GetNextContentIdAsync(ct);

        var (html, images) = _htmlService.ConvertBase64ImagesToURLImages(
            request.RawText,
            contentId,
            ImageFolder.Content,
            _imageService.GetImageURL());

        var text = _htmlService.ExtractTextFromHTML(html);

        var uploadResult = await _imageService.UploadMultipleImagesAsStreamAsync(images, ImageFolder.Content, ct);
        if (!uploadResult)
            return false;

        return await _answerRepository.AddAnswerAsync(
            contentId,
            request.IdQuestion,
            request.ParentQuestionId,
            request.IdLoggedUser,
            html, text,
            images.Keys.Select(imgName => $"{_imageService.GetImageURL()}{ImageFolder.Content.Value}/{contentId}/{imgName}"),
            request.CreationTime, ct);
    }
}