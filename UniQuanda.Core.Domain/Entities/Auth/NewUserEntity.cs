﻿using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.Auth;

public class NewUserEntity
{
    public string Nickname { get; set; }
    public string HashedPassword { get; set; }
    public UserOptionalInfo OptionalInfo { get; set; }
    public string Email { get; set; }
    public string EmailConfirmationToken { get; set; }
    public DateTime ExistsUntil { get; set; }
}