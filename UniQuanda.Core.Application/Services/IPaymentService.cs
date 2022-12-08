using UniQuanda.Core.Domain.Utils.PayU;

namespace UniQuanda.Core.Application.Services;

public interface IPaymentService
{
    /// <summary>
    ///     Get token to authorize
    /// </summary>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Token if success, otherwise null</returns>
    Task<string?> GetAuthenticationTokenAsync(CancellationToken ct);

    /// <summary>
    ///     Create order in payment provider
    /// </summary>
    /// <param name="price">Premium price</param>
    /// <param name="token">Authorization token</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Order information if success, otherwise null</returns>
    Task<OrderCreateResponse?> CreateOrderOfPremiumAsync(decimal price, string token, CancellationToken ct);

    /// <summary>
    ///     Get order information
    /// </summary>
    /// <param name="token">Authorization token</param>
    /// <param name="idPayment">Id premium payment</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Full order information if success, otherwise null</returns>
    Task<PayUModel?> GetOrderOfPremiumAsync(string token, string idPayment, CancellationToken ct);
}