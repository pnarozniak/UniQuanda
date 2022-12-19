using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.AddAnswer;

public class AddAnswerHandler : IRequestHandler<AddAnswerCommand, AddAnswerResponseDTO>
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

    public async Task<AddAnswerResponseDTO> Handle(AddAnswerCommand request, CancellationToken ct)
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
            return new AddAnswerResponseDTO
            {
                Status = false
            };

        var (isSuccessful, idAnswer) = await _answerRepository.AddAnswerAsync(
            contentId,
            request.IdQuestion,
            request.ParentQuestionId,
            request.IdLoggedUser,
            html, text,
            images.Keys.Select(imgName => $"{_imageService.GetImageURL()}{ImageFolder.Content.Value}/{contentId}/{imgName}"),
            request.CreationTime, ct);

        return new AddAnswerResponseDTO()
        {
            Status = isSuccessful,
            Page = isSuccessful ? await _answerRepository.GetAnswerPageAsync(request.IdQuestion, request.ParentQuestionId == null ? idAnswer.Value : request.ParentQuestionId.Value, ct) : 1,
            IdAnswer = request.ParentQuestionId ?? idAnswer,
            IdComment = idAnswer
        };
    }
}