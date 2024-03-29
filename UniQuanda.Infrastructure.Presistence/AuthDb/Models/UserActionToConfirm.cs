using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Models;

public class UserActionToConfirm
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public int? IdUserEmail { get; set; }
    public string ConfirmationToken { get; set; }
    public DateTime ExistsUntil { get; set; }
    public UserActionToConfirmEnum ActionType { get; set; }

    public virtual User IdUserNavigation { get; set; }
    public virtual UserEmail IdUserEmailNavigation { get; set; }
}