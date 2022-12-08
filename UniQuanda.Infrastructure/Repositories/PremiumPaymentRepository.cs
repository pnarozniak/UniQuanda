using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums.DbModel;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Core.Domain.Utils.PayU;
using UniQuanda.Core.Domain.ValueObjects;
using UniQuanda.Infrastructure.Options;
using UniQuanda.Infrastructure.Presistence.AuthDb;
using UniQuanda.Infrastructure.Presistence.AuthDb.Models;

namespace UniQuanda.Infrastructure.Repositories;

public class PremiumPaymentRepository : IPremiumPaymentRepository
{
    private readonly AuthDbContext _authContext;
    private readonly PayUOptions _options;

    public PremiumPaymentRepository(AuthDbContext authDbContext, PayUOptions options)
    {
        _authContext = authDbContext;
        _options = options;
    }
    public async Task<bool> AddPremiumPaymentAsync(OrderCreateResponse order, decimal price, int idUser, CancellationToken ct)
    {
        if (await _authContext.PremiumPayments.AnyAsync(p => p.IdPayment == order.OrderId, ct))
            return false;

        var payment = new PremiumPayment
        {
            IdPayment = order.OrderId,
            Price = price,
            PaymentStatus = PremiumPaymentStatusEnum.New,
            IdUser = idUser,
            PaymentUrl = order.RedirectUri,
            PaymentDate = DateTime.UtcNow,
            ValidUntil = DateTime.UtcNow.AddSeconds(_options.OrderCreateRequest.ValidityTime)
        };
        await _authContext.AddAsync(payment, ct);
        return await _authContext.SaveChangesAsync(ct) != 0;
    }

    public async Task<(bool isUserExists, DateTime? hasPremiumUntil)> GetUserPremiumInfoAsync(int idUser, CancellationToken ct)
    {
        var user = await _authContext.Users.SingleOrDefaultAsync(u => u.Id == idUser, ct);
        if (user is null)
            return (isUserExists: false, hasPremiumUntil: null);
        return (isUserExists: true, hasPremiumUntil: user.HasPremiumUntil);
    }

    public async Task<string?> CheckIfAnyPremiumPaymentIsStartedAsync(int idUser, CancellationToken ct)
    {
        var newPremiumPayment = await _authContext.PremiumPayments.SingleOrDefaultAsync(p => p.IdUser == idUser && p.PaymentStatus == PremiumPaymentStatusEnum.New, ct);
        if (newPremiumPayment is null)
            return null;

        if (newPremiumPayment.ValidUntil < DateTime.UtcNow)
        {
            newPremiumPayment.PaymentStatus = PremiumPaymentStatusEnum.Canceled;
            newPremiumPayment.ValidUntil = null;
            newPremiumPayment.PaymentUrl = null;
            await _authContext.SaveChangesAsync(ct);
            return null;
        }
        return newPremiumPayment.PaymentUrl;
    }

    public async Task<string?> GetPremiumPaymentIdAsync(int idUser, CancellationToken ct)
    {
        var newPremiumPayment = await _authContext.PremiumPayments.SingleOrDefaultAsync(p => p.IdUser == idUser && p.PaymentStatus == PremiumPaymentStatusEnum.New, ct);
        if (newPremiumPayment is null)
            return null;

        if (newPremiumPayment.ValidUntil < DateTime.UtcNow)
        {
            newPremiumPayment.PaymentStatus = PremiumPaymentStatusEnum.Canceled;
            newPremiumPayment.ValidUntil = null;
            newPremiumPayment.PaymentUrl = null;
            await _authContext.SaveChangesAsync(ct);
            return null;
        }
        return newPremiumPayment.IdPayment;
    }

    public async Task<UpdatePremiumPaymentResultEnum> UpdatePremiumPaymentAsync(PayUModel order, CancellationToken ct)
    {
        var userPremiumPayment = await _authContext.PremiumPayments.Include(p => p.IdUserNavigation).SingleOrDefaultAsync(p => p.IdPayment == order.Orders.First().OrderId, ct);
        if (userPremiumPayment is null)
            return UpdatePremiumPaymentResultEnum.ContentNotExist;

        Enum.TryParse(order.Orders.First().Status, true, out PremiumPaymentStatusEnum premiumPaymentStatus);
        if (premiumPaymentStatus == PremiumPaymentStatusEnum.New)
            return UpdatePremiumPaymentResultEnum.PaymentHasStatusNew;

        userPremiumPayment.PaymentStatus = premiumPaymentStatus;
        userPremiumPayment.IdTransaction = order.Properties.First().Value;
        userPremiumPayment.PaymentDate = order.Orders.First().OrderCreateDate;
        if (premiumPaymentStatus == PremiumPaymentStatusEnum.Completed)
        {
            userPremiumPayment.FirstName = order.Orders.First().Buyer.FirstName;
            userPremiumPayment.LastName = order.Orders.First().Buyer.LastName;
            userPremiumPayment.Email = order.Orders.First().Buyer.Email;

            var hasPremiumUntil = userPremiumPayment.IdUserNavigation.HasPremiumUntil;
            var hasPremium = hasPremiumUntil != null && hasPremiumUntil > DateTime.UtcNow;
            userPremiumPayment.IdUserNavigation.HasPremiumUntil = hasPremium ? hasPremiumUntil!.Value.AddMonths(1) : order.Orders.First().OrderCreateDate.AddMonths(1);
            userPremiumPayment.PaymentUrl = null;
            userPremiumPayment.ValidUntil = null;
            return await _authContext.SaveChangesAsync(ct) == 2 ? UpdatePremiumPaymentResultEnum.Successful : UpdatePremiumPaymentResultEnum.UnSuccessful;
        }

        return await _authContext.SaveChangesAsync(ct) == 1 ? UpdatePremiumPaymentResultEnum.Successful : UpdatePremiumPaymentResultEnum.UnSuccessful;
    }

    public async Task<PremiumPaymentsEntity?> GetPremiumPaymentsAsync(int idUser, bool getAll, CancellationToken ct)
    {
        if (getAll)
            return await _authContext.Users.Where(u => u.Id == idUser).Select(u => new PremiumPaymentsEntity
            {
                Nickname = u.Nickname,
                HasPremiumUntil = u.HasPremiumUntil,
                Payments = u.PremiumPayments.OrderByDescending(p => p.PaymentDate).Select(p => new PremiumPaymentInfo
                {
                    PaymentDate = p.PaymentDate,
                    IdTransaction = p.IdTransaction,
                    Price = p.Price,
                    PaymentStatus = p.PaymentStatus == PremiumPaymentStatusEnum.New && p.ValidUntil < DateTime.UtcNow ? PremiumPaymentStatusEnum.Canceled : p.PaymentStatus,
                    PaymentUrl = p.PaymentStatus == PremiumPaymentStatusEnum.New && p.ValidUntil > DateTime.UtcNow ? p.PaymentUrl : null
                })
            }).SingleOrDefaultAsync(ct);

        return await _authContext.Users.Where(u => u.Id == idUser).Select(u => new PremiumPaymentsEntity
        {
            Nickname = u.Nickname,
            HasPremiumUntil = u.HasPremiumUntil,
            Payments = u.PremiumPayments.OrderByDescending(p => p.PaymentDate).Select(p => new PremiumPaymentInfo
            {
                PaymentDate = p.PaymentDate,
                IdTransaction = p.IdTransaction,
                Price = p.Price,
                PaymentStatus = p.PaymentStatus == PremiumPaymentStatusEnum.New && p.ValidUntil < DateTime.UtcNow ? PremiumPaymentStatusEnum.Canceled : p.PaymentStatus,
                PaymentUrl = p.PaymentStatus == PremiumPaymentStatusEnum.New && p.ValidUntil > DateTime.UtcNow ? p.PaymentUrl : null
            }).Take(6),
            NumberOfPayments = u.PremiumPayments.Count
        }).SingleOrDefaultAsync(ct);
    }
}