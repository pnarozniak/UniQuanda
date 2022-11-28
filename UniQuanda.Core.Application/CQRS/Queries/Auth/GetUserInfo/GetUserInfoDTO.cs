namespace UniQuanda.Core.Application.CQRS.Queries.Auth.GetUserInfo;

public class GetUserInfoResponseDTO
{
		public string RefreshToken { get; set; }
		public string Nickname { get; set; }
		public string? Avatar { get; set; }
}