using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Domain.Entities.Auth;

public class UserActionToConfirmEntity
{
		public int Id { get; set; }
    public int IdUser { get; set; }
		public string ConfirmationToken { get; set; }
		public DateTime ExistsUntil { get; set; }
    public UserActionToConfirmEnum ActionType { get; set; }
}