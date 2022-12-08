namespace UniQuanda.Core.Domain.Utils.PayU;

public class OrderCreateResponse
{
    public OrderCreateResponseStatus Status { get; set; }
    public string RedirectUri { get; set; }
    public string OrderId { get; set; }
}

public class OrderCreateResponseStatus
{
    public string StatusCode { get; set; }
}