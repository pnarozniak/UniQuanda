using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResetPasword;

public class ResetPasswordCommand : IRequest<bool>
{
    public ResetPasswordCommand(ResetPaswordDTO request)
    {
				RecoveryToken = request.RecoveryToken;
        Email = request.Email;
				NewPassword = request.NewPassword;
    }

		public string RecoveryToken { get; set; }
    public string Email { get; set; }
		public string NewPassword { get; set; }
}