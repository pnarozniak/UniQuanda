using System.Text.Json.Serialization;

namespace UniQuanda.Core.Domain.Utils.PayU;

public class PayUOAuthInfo
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; }
}
