namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdatePassword;

public class UpdatePasswordRequestDTO
{
    public string NewPassword { get; set; }
    public string OldPassword { get; set; }
}