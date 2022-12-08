using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Options;

public class PayUOptions
{
    public PayUOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection("PayU");
        OAuth = new PayUOAuthOptions(section.GetSection("OAuth"));
        Endpoints = new PayUEndpointsOptions(section.GetSection("Endpoints"));
        OrderCreateRequest = new PayUOrderCreateRequestOptions(section.GetSection("OrderCreateRequest"));
    }

    public PayUOAuthOptions OAuth { get; set; }
    public PayUEndpointsOptions Endpoints { get; set; }
    public PayUOrderCreateRequestOptions OrderCreateRequest { get; set; }
}
public class PayUOAuthOptions
{
    public PayUOAuthOptions(IConfigurationSection section)
    {
        GrantType = section["GrantType"];
        ClientId = section["ClientId"];
        ClientSecret = section["ClientSecret"];
    }

    public string GrantType { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}

public class PayUEndpointsOptions
{
    public PayUEndpointsOptions(IConfigurationSection section)
    {
        OAuthUrl = section["OAuthUrl"];
        OrderCreateRequestUrl = section["OrderCreateRequestUrl"];
        OrderRetrieveRequestUrl = section["OrderRetrieveRequestUrl"];
    }

    public string OAuthUrl { get; set; }
    public string OrderCreateRequestUrl { get; set; }
    public string OrderRetrieveRequestUrl { get; set; }
}


public class PayUOrderCreateRequestOptions
{
    public PayUOrderCreateRequestOptions(IConfigurationSection section)
    {
        MerchantPosId = section["MerchantPosId"];
        CurrencyCode = section["CurrencyCode"];
        ContinueUrl = section["ContinueUrl"];
        ValidityTime = Int32.Parse(section["ValidityTime"]);
        PayMethod = new PayUPayMethodOptions(section.GetSection("PayMethod"));
    }

    public string MerchantPosId { get; set; }
    public string CurrencyCode { get; set; }
    public string ContinueUrl { get; set; }
    public int ValidityTime { get; set; }
    public PayUPayMethodOptions PayMethod { get; set; }
}

public class PayUPayMethodOptions
{
    public PayUPayMethodOptions(IConfigurationSection section)
    {
        Type = section["Type"];
        Value = section["Value"];
    }

    public string Type { get; set; }
    public string Value { get; set; }
}