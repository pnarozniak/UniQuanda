using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Core.Domain.Utils.PayU;

namespace UniQuanda.Core.Application.Repositories;

public interface IPremiumPaymentRepository
{
    /// <summary>
    ///     Save created premium payment
    /// </summary>
    /// <param name="order">Data from payment provider</param>
    /// <param name="price">Price of premium</param>
    /// <param name="idUser">Id user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Status of add</returns>
    Task<bool> AddPremiumPaymentAsync(OrderCreateResponse order, decimal price, int idUser, CancellationToken ct);

    /// <summary>
    ///     Return user premium information
    /// </summary>
    /// <param name="idUser">Id user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Info if user exists and if exists additionaly until when has premium</returns>
    Task<(bool isUserExists, DateTime? hasPremiumUntil)> GetUserPremiumInfoAsync(int idUser, CancellationToken ct);

    /// <summary>
    ///     Check if is any payment with status NEW
    /// </summary>
    /// <param name="idUser">Id user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Payment url of existing payment with status NEW</returns>
    Task<string?> CheckIfAnyPremiumPaymentIsStartedAsync(int idUser, CancellationToken ct);

    /// <summary>
    ///     Get premium payment id
    /// </summary>
    /// <param name="idUser">Id user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>If exists premium payment id, otherwise null</returns>
    Task<string?> GetPremiumPaymentIdAsync(int idUser, CancellationToken ct);

    /// <summary>
    ///     Update premium payment data
    /// </summary>
    /// <param name="order">Order information</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Enum status of update</returns>
    Task<UpdatePremiumPaymentResultEnum> UpdatePremiumPaymentAsync(PayUModel order, CancellationToken ct);

    /// <summary>
    ///     Get user premium payments
    /// </summary>
    /// <param name="idUser">Id user</param>
    /// <param name="getAll">If should get all payments</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>If exists user info plus premium payments, otherwise null</returns>
    Task<PremiumPaymentsEntity?> GetPremiumPaymentsAsync(int idUser, bool getAll, CancellationToken ct);
}