using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Questions.UpdateQuestion;

public class UpdateQuestionHandler : IRequestHandler<UpdateQuestionCommand, bool?>
{
    private readonly IImageService _imageService;
    private readonly IHtmlService _htmlService;
    private readonly IContentRepository _contentRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly ITagRepository _tagRepository;

    public UpdateQuestionHandler(
        IImageService imageService,
        IHtmlService htmlService,
        IContentRepository contentRepository,
        ITagRepository tagRepository,
        IQuestionRepository questionRepository)
    {
        _imageService = imageService;
        _htmlService = htmlService;
        _contentRepository = contentRepository;
        _tagRepository = tagRepository;
        _questionRepository = questionRepository;
    }

    public async Task<bool?> Handle(UpdateQuestionCommand request, CancellationToken ct)
    {
        var contentId = await _contentRepository.GetIdContentOfQuestionAsync(request.IdQuestion, ct);
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
            _imageService.GetImageURL()
        );
        var text = _htmlService.ExtractTextFromHTML(html);
        if (!await _tagRepository.CheckIfAllTagIdsExistAsync(request.Tags, ct))
            return false;
        var tags = request.Tags.Select((tag, index) => (index, tag));
        await _imageService.UploadMultipleImagesAsStreamAsync(images, ImageFolder.Content, ct);

        return await _questionRepository.UpdateQuestionAsync(request.IdQuestion, contentId.Value, request.UserId, tags, request.Title, html, text, images.Keys.Select(imgName => $"{_imageService.GetImageURL()}{ImageFolder.Content.Value}/{contentId}/{imgName}"), request.CreationTime, ct); ;
    }
}