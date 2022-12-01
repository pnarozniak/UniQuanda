using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Infrastructure.Options;

namespace UniQuanda.Infrastructure.Services.Auth;

public class OAuthService : IOAuthService
{
    private readonly OAuthOptions _oAuthOptions;
    private readonly UniQuandaClientOptions _uniquandaClientOptions;

    public OAuthService(OAuthOptions oAuthOptions, UniQuandaClientOptions uniQuandaClientOptions)
    {
        _oAuthOptions = oAuthOptions;
        _uniquandaClientOptions = uniQuandaClientOptions;
    }

    public async Task<GoogleIdToken?> GetGoogleIdTokenAsync(string code)
    {
        try
        {
            using var http = new HttpClient();
            var url = _oAuthOptions.GoogleOAuth.TokenUrl;

            var clientId = _oAuthOptions.GoogleOAuth.ClientId;
            var clientSecret = _oAuthOptions.GoogleOAuth.ClientSecret;
            var redirectUri = _oAuthOptions.GoogleOAuth.RedirectUri;
            var grantType = "authorization_code";

            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>(){
                            {"code", code},
                            {"client_id", clientId},
                            {"client_secret", clientSecret},
                            {"redirect_uri", redirectUri},
                            {"grant_type", grantType},
                        })
            };
            var response = await http.SendAsync(req);
            if (response.StatusCode != HttpStatusCode.OK) return null;

            var resBody = await response.Content.ReadFromJsonAsync<GoogleTokenResponse>();
            if (resBody is null) return null;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(resBody.id_token);
            var jwt = jsonToken as JwtSecurityToken;
            if (jwt is null) return null;

            return new GoogleIdToken()
            {
                Id = jwt.Claims.First(c => c.Type == "sub").Value,
                Email = jwt.Claims.First(c => c.Type == "email").Value,
            };
        }
        catch
        {
            return null;
        }
    }

    public string GetGoogleClientHandlerUrl()
    {
        return $"{_uniquandaClientOptions.Url}/public/login/oauth/Google";
    }

    private class GoogleTokenResponse
    {
        public string id_token { get; set; }
    }
}

