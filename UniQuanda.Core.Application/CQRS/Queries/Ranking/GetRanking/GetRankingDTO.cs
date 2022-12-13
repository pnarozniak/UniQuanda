using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.Ranking.GetRanking;

public class GetRankingRequestDTO
{
    [Required]
    public int Page { get; set; }
    [Required]
    public bool AddPagesCount { get; set; }
    public int? TagId { get; set; }
}

public class GetRankingResponseDTO
{
    public IEnumerable<GetRankingResponseDTOUser> RankingPage { get; set; }
    public int? PagesCount { get; set; }
}

public class GetRankingResponseDTOUser
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string? Avatar { get; set; }
    public int Points { get; set; }
    public int Place { get; set; }
    public IEnumerable<GetRankingResponseDTOUserAcademicTitle> Titles { get; set; }
}

public class GetRankingResponseDTOUserAcademicTitle
{
    public string Name { get; set; }
    public int Order { get; set; }
    public AcademicTitleEnum AcademicTitleType { get; set; }
}