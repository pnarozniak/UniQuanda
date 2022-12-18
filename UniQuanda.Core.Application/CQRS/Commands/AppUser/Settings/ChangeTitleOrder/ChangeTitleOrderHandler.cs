using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Settings.ChangeTitleOrder
{
    public class ChangeTitleOrderHandler : IRequestHandler<ChangeTitleOrderCommand, bool>
    {
        private readonly IAcademicTitleRepository _academicTitleRepository;
        public ChangeTitleOrderHandler(IAcademicTitleRepository academicTitleRepository)
        {
            _academicTitleRepository = academicTitleRepository;
        }
        public async Task<bool> Handle(ChangeTitleOrderCommand request, CancellationToken ct)
        {
            var curentUserTitles = await _academicTitleRepository.GetAcademicTitlesOfUserAsync(request.UserId, ct);
            if (request.TitlesWithOrders.Values.Any(providedUserTitle => 
                !curentUserTitles.Any(title => 
                    title.Id == providedUserTitle)
                )
             || request.TitlesWithOrders.Values.Count != curentUserTitles.ToList().Count)
                return false;
            return await _academicTitleRepository.SaveOrderOfAcademicTitleForUserAsync(request.UserId, request.TitlesWithOrders, ct);
        }
    }
}
