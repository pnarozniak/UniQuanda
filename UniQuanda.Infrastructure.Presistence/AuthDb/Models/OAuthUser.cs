using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Models;

public class OAuthUser
{
    public int IdUser { get; set; }
		public string OAuthId { get; set; }
    public OAuthProviderEnum OAuthProvider { get; set; }
    public string? OAuthRegisterConfirmationCode { get; set; }

    public virtual User IdUserNavigation { get; set; }
}