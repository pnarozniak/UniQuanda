using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Ranking.GetRanking;

public class GetRankingQuery : IRequest<GetRankingResponseDTO>
{
    public GetRankingQuery(GetRankingRequestDTO request)
    {
        var pageSize = 10;
        Page = request.Page;
        AddCount = request.AddPagesCount;
        TagId = request.TagId;
        Take = pageSize;
        Skip = (Page - 1) * pageSize;

    }
    public int Take { get; set; }
    public int Skip { get; set; }
    public int Page { get; set; }
    public int? TagId { get; set; }
    public bool AddCount { get; set; }
}