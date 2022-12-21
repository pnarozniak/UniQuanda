using MediatR;
using UniQuanda.Core.Application.CQRS.Commands.Questions.AddQuestion;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Enums.Results;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class AddQuestionHandler : IRequestHandler<AddQuestionCommand, AddQuestionResponseDTO>
{
    private readonly IImageService _imageService;
    private readonly IHtmlService _htmlService;
    private readonly IContentRepository _contentRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IRoleRepository _roleRepository;

    public AddQuestionHandler(
        IImageService imageService,
        IHtmlService htmlService,
        IContentRepository contentRepository,
        ITagRepository tagRepository,
        IQuestionRepository questionRepository,
        IRoleRepository roleRepository
    )
    {
        _imageService = imageService;
        _htmlService = htmlService;
        _contentRepository = contentRepository;
        _tagRepository = tagRepository;
        _questionRepository = questionRepository;
        _roleRepository = roleRepository;

    }

    public async Task<AddQuestionResponseDTO> Handle(AddQuestionCommand request, CancellationToken ct)
    {
        var getQuestionPermission = await _roleRepository.GetExecutesOfPermissionByUserAsync(request.UserId, "ask-question", ct);
        if (getQuestionPermission.maxAmount == 0)
            return new() { Status = AskQuestionResultEnum.PermissionDenied };
        if (getQuestionPermission.maxAmount != null && getQuestionPermission.maxAmount == getQuestionPermission.usedAmount)
            return new() { Status = AskQuestionResultEnum.LimitsExceeded };
        var contentId = await _contentRepository.GetNextContentIdAsync(ct);

        var (html, images) = _htmlService.ConvertBase64ImagesToURLImages(
            request.RawText,
            contentId,
            ImageFolder.Content,
            _imageService.GetImageURL()
        );
        var text = _htmlService.ExtractTextFromHTML(html);
        if (!await _tagRepository.CheckIfAllTagIdsExistAsync(request.Tags, ct)) return new()
        {
            Status = AskQuestionResultEnum.TagsNotFound
        };
        var tags = request.Tags.Select((tag, index) => (index, tag));
        await _imageService.UploadMultipleImagesAsStreamAsync(
            images,
            ImageFolder.Content,
            ct
        );
        var questionId = await _questionRepository.AddQuestionAsync(
            contentId, request.UserId,
            tags, request.Title,
            html, text,
            images.Keys
                .Select(imgName => $"{_imageService.GetImageURL()}{ImageFolder.Content.Value}/{contentId}/{imgName}"),
            request.CreationTime, ct);
        if (questionId == 0) return new()
        {
            Status = AskQuestionResultEnum.Error
        };
        await _roleRepository.AddExecutionOfPermissionToUserAsync(request.UserId, "ask-question", ct);

        return new()
        {
            QuestionId = questionId,
            Status = AskQuestionResultEnum.QuestionAsked
        };
    }
}