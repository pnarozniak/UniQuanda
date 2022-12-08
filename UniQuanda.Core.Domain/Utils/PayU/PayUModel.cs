namespace UniQuanda.Core.Domain.Utils.PayU;

public class PayUModel
{
    public List<PayUOrder> Orders { get; set; }
    public PayUStatus Status { get; set; }
    public List<PayUProperty> Properties { get; set; }
}

public class PayUOrder
{
    public string OrderId { get; set; }
    public string? ExtOrderId { get; set; }
    public DateTime OrderCreateDate { get; set; }
    public string NotifyUrl { get; set; }
    public string CustomerIp { get; set; }
    public string MerchantPosId { get; set; }
    public string Description { get; set; }
    public string CurrencyCode { get; set; }
    public int TotalAmount { get; set; }
    public string ContinueUrl { get; set; }
    public string Status { get; set; }
    public int ValidityTime { get; set; }
    public PayUBuyer Buyer { get; set; }
    public PayMethods PayMethods { get; set; }
    public List<PayUProduct> Products { get; set; }
}

public class PayUBuyer
{
    public string CustomerId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Language { get; set; }
}

public class PayMethods
{
    public PayMethod PayMethod { get; set; }
}

public class PayMethod
{
    public string Type { get; set; }
    public string Value { get; set; }
}

public class PayUProduct
{
    public string Name { get; set; }
    public int UnitPrice { get; set; }
    public int Quantity { get; set; }
    public bool Virtual { get; set; }
}

public class PayUStatus
{
    public string StatusCode { get; set; }
    public string StatusDesc { get; set; }
}

public class PayUProperty
{
    public string Name { get; set; }
    public string Value { get; set; }
}