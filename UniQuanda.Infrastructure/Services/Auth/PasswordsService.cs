using UniQuanda.Core.Application.Services.Auth;

namespace UniQuanda.Infrastructure.Services.Auth;

public class PasswordsService : IPasswordsService
{
    public string HashPassword(string plainPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }

    public bool VerifyPassword(string plainPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }
}