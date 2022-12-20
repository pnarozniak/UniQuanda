using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.UpdateAnswer;

public class UpdateAnswerHandler : IRequestHandler<UpdateAnswerCommand, bool?>
{
    private readonly IImageService _imageService;
    private readonly IHtmlService _htmlService;
    private readonly IContentRepository _contentRepository;
    private readonly IAnswerRepository _answerRepository;

    public UpdateAnswerHandler(
        IImageService imageService,
        IHtmlService htmlService,
        IContentRepository contentRepository,
        IAnswerRepository answerRepository)
    {
        _imageService = imageService;
        _htmlService = htmlService;
        _contentRepository = contentRepository;
        _answerRepository = answerRepository;
    }

    public async Task<bool?> Handle(UpdateAnswerCommand request, CancellationToken ct)
    {
        var contentId = await _contentRepository.GetIdContentOfAnswerAsync(request.IdAnswer, ct);
        if (contentId is null)
            return null;

        var previousImagesUrl = await _contentRepository.GetAllUrlImagesConnectedWithContent(contentId.Value, ct);
        foreach (var image in previousImagesUrl)
        {
            var arrImg = image.Split('/');
            await _imageService.RemoveImageAsync($"{contentId}/{arrImg[^1]}", ImageFolder.Content, ct);
        }

        var (html, images) = _htmlService.ConvertBase64ImagesToURLImages(
            request.RawText,
            contentId.Value,
            ImageFolder.Content,
            _imageService.GetImageURL());

        var text = _htmlService.ExtractTextFromHTML(html);

        var uploadResult = await _imageService.UploadMultipleImagesAsStreamAsync(images, ImageFolder.Content, ct);
        if (!uploadResult)
            return false;

        return await _answerRepository.UpdateAnswerAsync(
            contentId.Value,
            request.IdAnswer,
            request.IdLoggedUser,
            html,
            text,
            images.Keys.Select(imgName => $"{_imageService.GetImageURL()}{ImageFolder.Content.Value}/{contentId}/{imgName}"),
            request.CreationTime,
            ct);
    }
}