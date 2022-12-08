namespace UniQuanda.Core.Application.CQRS.Queries.Auth.GetReportTypes;

public class GetTop5UsersResponseDTO
{
    public IEnumerable<AppUserInRankingResponseDTO> Top5Users { get; set; }
}

public class AppUserInRankingResponseDTO
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string? Avatar { get; set; }
    public int Points { get; set; }
}