using UniQuanda.Core.Domain.Enums.DbModel;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Models;

public class Product
{
    public ProductTypeEnum ProductType { get; set; }
    public decimal Price { get; set; }
}
