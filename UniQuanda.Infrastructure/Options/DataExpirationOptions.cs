﻿using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Options;

public class DataExpirationOptions
{
    public DataExpirationOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection("DataExpiration");
        NewUserExpirationInHours = int.Parse(section["NewUserExpirationInHours"]);
        RecoverPasswordActionExpirationInMinutes = int.Parse(section["RecoverPasswordActionExpirationInMinutes"]);
        EmailConfirmationExpirationInHours = int.Parse(section["EmailConfirmationExpirationInHours"]);
    }

    public int NewUserExpirationInHours { get; set; }
    public int RecoverPasswordActionExpirationInMinutes { get; set; }
    public int EmailConfirmationExpirationInHours { get; set; }
}