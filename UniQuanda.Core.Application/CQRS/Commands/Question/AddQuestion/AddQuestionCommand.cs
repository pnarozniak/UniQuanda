using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Question.AddQuestion
{
    public class AddQuestionCommand : IRequest<bool>
    {
        public AddQuestionCommand(AddQuestionRequestDTO request)
        {
            this.Id = request.Id;
            this.Title = request.Title;
            this.Content = request.Content;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class AddQuestionHandler : IRequestHandler<AddQuestionCommand, bool>
    {
        private readonly IQuestionRepository _repository;
        public AddQuestionHandler(IQuestionRepository repository)
        {
            this._repository = repository;
        }

        public async Task<bool> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddQuestionAsync(new Domain.Entities.Question
            {
                Id = request.Id,
                Title = request.Title,
                Content = request.Content
            });
        }
    }
}
