using MediatR;
using UniQuanda.Core.Application.CQRS.Commands.Questions.AddQuestion;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class AddQuestionHandler : IRequestHandler<AddQuestionCommand, AddQuestionResponseDTO>
{
    private readonly IImageService _imageService;
    private readonly IHtmlService _htmlService;
    private readonly IContentRepository _contentRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly ITagRepository _tagRepository;

    public AddQuestionHandler(
        IImageService imageService, 
        IHtmlService htmlService, 
        IContentRepository contentRepository, 
        ITagRepository tagRepository,
        IQuestionRepository questionRepository
    )
    {
        _imageService = imageService;
        _htmlService = htmlService;
        _contentRepository = contentRepository;
        _tagRepository = tagRepository;
        _questionRepository = questionRepository;

    }

    public async Task<AddQuestionResponseDTO> Handle(AddQuestionCommand request, CancellationToken ct)
    {
        var contentId = await _contentRepository.GetNextContentIdAsync();
        var (html, images) = _htmlService.ConvertBase64ImagesToURLImages(
            request.RawText, 
            contentId, 
            ImageFolder.Content, 
            _imageService.GetImageURL()
        );
        var text = _htmlService.ExtractTextFromHTML(html);
        if (!await _tagRepository.CheckIfAllTagIdsExistAsync(request.Tags, ct)) return new()
        {
            QuestionId = null
        };
        var tags = request.Tags.Select((tag, index) => (index, tag));
        var imageIds = await _imageService.UploadMultipleImagesAsync(images, ImageFolder.Content, ct);
        var questionId = await _questionRepository.AddQuestionAsync(
            contentId, request.UserId,
            tags, request.Title, 
            html, text, imageIds, ct);
        return new()
        {
            QuestionId = questionId
        };
    }
}