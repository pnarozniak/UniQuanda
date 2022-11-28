using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Models;

public class User
{
    public int Id { get; set; }
    public string? Nickname { get; set; }
    public string HashedPassword { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }

    public virtual OAuthUser? IdOAuthUserNavigation { get; set; }
    public virtual TempUser IdTempUserNavigation { get; set; }
    public virtual ICollection<UserEmail> Emails { get; set; }
    public virtual ICollection<UserActionToConfirm> ActionsToConfirm { get; set; }
}