using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Enums.DbModel;
using UniQuanda.Infrastructure.Presistence.AppDb;

namespace UniQuanda.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        _appContext = appDbContext;
    }

    public async Task<decimal?> GetPremiumPriceAsync(CancellationToken ct)
    {
        var product = await _appContext.Products.SingleOrDefaultAsync(p => p.ProductType == ProductTypeEnum.Premium, ct);
        return product?.Price;
    }
}