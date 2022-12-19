
namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Settings.ChangeTitleOrder
{
    public class ChangeTitleOrderRequestDTO
    {
        public int TitleId { get; set; }
        public int Order { get; set; }
    }
}
