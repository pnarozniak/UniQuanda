using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums.DbModel;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Core.Domain.Utils.PayU;
using UniQuanda.Core.Domain.ValueObjects;
using UniQuanda.Infrastructure.Options;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Repositories;

public class PremiumPaymentRepository : IPremiumPaymentRepository
{
    private readonly AppDbContext _context;
    private readonly PayUOptions _options;

    public PremiumPaymentRepository(AppDbContext context, PayUOptions options)
    {
        _context = context;
        _options = options;
    }
    public async Task<bool> AddPremiumPaymentAsync(OrderCreateResponse order, decimal price, int idUser, CancellationToken ct)
    {
        if (await _context.PremiumPayments.AnyAsync(p => p.IdPayment == order.OrderId, ct))
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
        await _context.AddAsync(payment, ct);
        return await _context.SaveChangesAsync(ct) != 0;
    }

    public async Task<string?> CheckIfAnyPremiumPaymentIsStartedAsync(int idUser, CancellationToken ct)
    {
        var newPremiumPayment = await _context.PremiumPayments.SingleOrDefaultAsync(p => p.IdUser == idUser && p.PaymentStatus == PremiumPaymentStatusEnum.New, ct);
        if (newPremiumPayment is null)
            return null;

        if (newPremiumPayment.ValidUntil < DateTime.UtcNow)
        {
            newPremiumPayment.PaymentStatus = PremiumPaymentStatusEnum.Canceled;
            newPremiumPayment.ValidUntil = null;
            newPremiumPayment.PaymentUrl = null;
            await _context.SaveChangesAsync(ct);
            return null;
        }
        return newPremiumPayment.PaymentUrl;
    }

    public async Task<string?> GetPremiumPaymentIdAsync(int idUser, CancellationToken ct)
    {
        var newPremiumPayment = await _context.PremiumPayments.SingleOrDefaultAsync(p => p.IdUser == idUser && p.PaymentStatus == PremiumPaymentStatusEnum.New, ct);
        if (newPremiumPayment is null)
            return null;

        if (newPremiumPayment.ValidUntil < DateTime.UtcNow)
        {
            newPremiumPayment.PaymentStatus = PremiumPaymentStatusEnum.Canceled;
            newPremiumPayment.ValidUntil = null;
            newPremiumPayment.PaymentUrl = null;
            await _context.SaveChangesAsync(ct);
            return null;
        }
        return newPremiumPayment.IdPayment;
    }

    public async Task<UpdatePremiumPaymentResultEnum> UpdatePremiumPaymentAsync(PayUModel order, CancellationToken ct)
    {
        var userPremiumPayment = await _context.PremiumPayments.Include(p => p.IdUserNavigation).SingleOrDefaultAsync(p => p.IdPayment == order.Orders.First().OrderId, ct);
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

            var role = await _context.UserRoles
                .Where(ur => ur.AppUserId == userPremiumPayment.IdUserNavigation.Id && ur.RoleIdNavigation.Name == AppRole.Premium)
                .FirstOrDefaultAsync(ct);

            if(role == null)
            {
                role = new UserRole()
                {
                    AppUserId = userPremiumPayment.IdUser,
                    ValidUnitl = order.Orders.First().OrderCreateDate.AddMonths(1),
                    RoleIdNavigation = await _context.Roles.Where(r => r.Name == AppRole.Premium).FirstOrDefaultAsync(ct)
                };
                await _context.UserRoles.AddAsync(role, ct);
            } else
            {
                var hasPremium = role.ValidUnitl > DateTime.UtcNow;
                role.ValidUnitl = hasPremium ? role.ValidUnitl.Value.AddMonths(1) : order.Orders.First().OrderCreateDate.AddMonths(1);
            }
            
            userPremiumPayment.PaymentUrl = null;
            userPremiumPayment.ValidUntil = null;
            return await _context.SaveChangesAsync(ct) == 2 ? UpdatePremiumPaymentResultEnum.Successful : UpdatePremiumPaymentResultEnum.UnSuccessful;
        }

        return await _context.SaveChangesAsync(ct) == 1 ? UpdatePremiumPaymentResultEnum.Successful : UpdatePremiumPaymentResultEnum.UnSuccessful;
    }

    public async Task<PremiumPaymentsEntity?> GetPremiumPaymentsAsync(int idUser, bool getAll, CancellationToken ct)
    {
        var query = _context.UserRoles
           .Where(ur => ur.AppUserId == idUser && ur.RoleIdNavigation.Name == AppRole.Premium);
        if (getAll)
            return await query.Select(ur => new PremiumPaymentsEntity()
            {
                Nickname = ur.AppUserIdNavigation.Nickname,
                HasPremiumUntil = ur.ValidUnitl,
                Payments = ur.AppUserIdNavigation.PremiumPayments.OrderByDescending(p => p.PaymentDate).Select(p => new PremiumPaymentInfo
                {
                    PaymentDate = p.PaymentDate,
                    IdTransaction = p.IdTransaction,
                    Price = p.Price,
                    PaymentStatus = p.PaymentStatus == PremiumPaymentStatusEnum.New && p.ValidUntil < DateTime.UtcNow ? PremiumPaymentStatusEnum.Canceled : p.PaymentStatus,
                    PaymentUrl = p.PaymentStatus == PremiumPaymentStatusEnum.New && p.ValidUntil > DateTime.UtcNow ? p.PaymentUrl : null
                })
            }).FirstOrDefaultAsync(ct);

        return await query.Select(ur => new PremiumPaymentsEntity()
        {
            Nickname = ur.AppUserIdNavigation.Nickname,
            HasPremiumUntil = ur.ValidUnitl,
            Payments = ur.AppUserIdNavigation.PremiumPayments.OrderByDescending(p => p.PaymentDate).Select(p => new PremiumPaymentInfo
            {
                PaymentDate = p.PaymentDate,
                IdTransaction = p.IdTransaction,
                Price = p.Price,
                PaymentStatus = p.PaymentStatus == PremiumPaymentStatusEnum.New && p.ValidUntil < DateTime.UtcNow ? PremiumPaymentStatusEnum.Canceled : p.PaymentStatus,
                PaymentUrl = p.PaymentStatus == PremiumPaymentStatusEnum.New && p.ValidUntil > DateTime.UtcNow ? p.PaymentUrl : null
            }).Take(6),
            NumberOfPayments = ur.AppUserIdNavigation.PremiumPayments.Count
        }).FirstOrDefaultAsync(ct);
    }
}