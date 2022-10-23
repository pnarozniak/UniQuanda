using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class UserActionToConfirm
{
    public int Id { get; set; }
    public int IdUser { get; set; }
		public string ConfirmationToken { get; set; }
		public DateTime ExistsUntil { get; set; }
    public UserActionToConfirmEnum ActionType { get; set; }

		public virtual User IdUserNavigation { get; set; }
}