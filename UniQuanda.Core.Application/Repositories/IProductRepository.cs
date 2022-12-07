namespace UniQuanda.Core.Application.Repositories;

public interface IProductRepository
{
    /// <summary>
    ///     Get price of premium
    /// </summary>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>If exists then price, otherwise null</returns>
    Task<decimal?> GetPremiumPriceAsync(CancellationToken ct);
}