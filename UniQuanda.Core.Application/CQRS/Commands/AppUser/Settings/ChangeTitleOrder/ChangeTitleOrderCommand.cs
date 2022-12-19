using MediatR;
namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Settings.ChangeTitleOrder
{
    public class ChangeTitleOrderCommand : IRequest<bool>
    {
        public ChangeTitleOrderCommand(IEnumerable<ChangeTitleOrderRequestDTO> request, int userId)
        {
            TitlesWithOrders = request.ToDictionary(x => x.Order, x => x.TitleId);
            UserId = userId;
        }
        public IDictionary<int, int> TitlesWithOrders { get; set; }
        public int UserId { get; set; }
    }
}
