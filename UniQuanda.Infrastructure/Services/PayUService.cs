using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Utils.PayU;
using UniQuanda.Infrastructure.Options;

namespace UniQuanda.Infrastructure.Services;

public class PayUService : IPaymentService
{
    private readonly PayUOptions _options;

    public PayUService(PayUOptions options)
    {
        _options = options;
    }

    public async Task<string?> GetAuthenticationTokenAsync(CancellationToken ct)
    {
        try
        {
            var url = _options.Endpoints.OAuthUrl;
            using var http = new HttpClient();

            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>(){
                            {"grant_type", _options.OAuth.GrantType},
                            {"client_id", _options.OAuth.ClientId},
                            {"client_secret", _options.OAuth.ClientSecret}
                        })
            };
            var response = await http.SendAsync(req, ct);

            if (!response.IsSuccessStatusCode)
                return null;

            var resBody = await response.Content.ReadFromJsonAsync<PayUOAuthInfo>(cancellationToken: ct);
            if (resBody is null)
                return null;

            return resBody.AccessToken;
        }
        catch
        {
            return null;
        }
    }

    public async Task<OrderCreateResponse?> CreateOrderOfPremiumAsync(decimal price, string token, CancellationToken ct)
    {
        try
        {
            var url = _options.Endpoints.OrderCreateRequestUrl;
            HttpClientHandler httpClientHandler = new();
            httpClientHandler.AllowAutoRedirect = false;
            using var http = new HttpClient(httpClientHandler);
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await http.PostAsJsonAsync(url, SetupOrderCreateRequest(price), ct);
            if (response.StatusCode != HttpStatusCode.Redirect)
                return null;

            var resBody = await response.Content.ReadFromJsonAsync<OrderCreateResponse>(cancellationToken: ct);
            if (resBody is null)
                return null;

            return resBody;
        }
        catch
        {
            return null;
        }
    }

    public PayUOrder SetupOrderCreateRequest(decimal price, int quantity = 1)
    {
        return new PayUOrder
        {
            CustomerIp = "127.0.0.1",
            MerchantPosId = _options.OrderCreateRequest.MerchantPosId,
            Description = "UniQuanda Premium",
            CurrencyCode = _options.OrderCreateRequest.CurrencyCode,
            ContinueUrl = _options.OrderCreateRequest.ContinueUrl,
            TotalAmount = (int)(price * 100 * quantity),
            ValidityTime = _options.OrderCreateRequest.ValidityTime,
            PayMethods = new PayMethods
            {
                PayMethod = new PayMethod
                {
                    Type = _options.OrderCreateRequest.PayMethod.Type,
                    Value = _options.OrderCreateRequest.PayMethod.Value
                }
            },
            Products = new()
            {
                new PayUProduct
                {
                    Name = "UniQuanda Premium",
                    UnitPrice = (int)(price * 100),
                    Quantity = quantity,
                    Virtual = true
                }
            }
        };
    }

    public async Task<PayUModel?> GetOrderOfPremiumAsync(string token, string idPayment, CancellationToken ct)
    {
        try
        {
            var url = _options.Endpoints.OrderRetrieveRequestUrl + idPayment;
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await http.GetAsync(url, ct);
            if (!response.IsSuccessStatusCode)
                return null;

            var resBody = await response.Content.ReadFromJsonAsync<PayUModel>(cancellationToken: ct);
            if (resBody is null)
                return null;

            return resBody;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}
