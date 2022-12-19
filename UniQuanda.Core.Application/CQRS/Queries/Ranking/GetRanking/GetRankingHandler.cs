using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Ranking.GetRanking
{
    public class GetRankingHandler : IRequestHandler<GetRankingQuery, GetRankingResponseDTO>
    {
        private readonly IRankingRepository _rankingRepository;

        public GetRankingHandler(IRankingRepository rankingRepository)
        {
            _rankingRepository = rankingRepository;
        }

        public async Task<GetRankingResponseDTO> Handle(GetRankingQuery request, CancellationToken ct)
        {
            return request.TagId != null ? await GetTagRanking(request, ct) : await GetGlobalRanking(request, ct);
        }

        private async Task<GetRankingResponseDTO> GetGlobalRanking(GetRankingQuery request, CancellationToken ct)
        {
            var ranking = await _rankingRepository.GetGlobalRankingUsersAsync(request.Take, request.Skip, ct);
            int? count = request.AddCount ? await _rankingRepository.GetGlobalRankingCountAsync(ct) : null;
            return new()
            {
                PagesCount = count != null ? (int)Math.Ceiling((double)count / request.Take) : null,
                RankingPage = ranking.Select(r => new GetRankingResponseDTOUser()
                {
                    Id = r.Id,
                    Nickname = r.Nickname,
                    Avatar = r.Avatar,
                    Place = r.PlaceInRanking ?? 0,
                    Points = r.Points ?? 0,
                    Titles = r.Titles.Select(t => new GetRankingResponseDTOUserAcademicTitle()
                    {
                        Name = t.Name,
                        AcademicTitleType = t.AcademicTitleType,
                        Order = t.Order
                    })
                })
            };
        }

        private async Task<GetRankingResponseDTO> GetTagRanking(GetRankingQuery request, CancellationToken ct)
        {
            var ranking = await _rankingRepository.GetTagRankingUsersAsync(request.TagId ?? 0, request.Page, request.Take, ct);
            int? count = request.AddCount ? await _rankingRepository.GetTagRankingCountAsync(request.TagId ?? 0, ct) : null;
            return new()
            {
                PagesCount = count != null ? (int)Math.Ceiling((double)count / request.Take) : null,
                RankingPage = ranking.Select(r => new GetRankingResponseDTOUser()
                {
                    Id = r.Id,
                    Nickname = r.Nickname,
                    Avatar = r.Avatar,
                    Place = r.PlaceInRanking ?? 0,
                    Points = r.Points ?? 0,
                    Titles = r.Titles.Select(t => new GetRankingResponseDTOUserAcademicTitle()
                    {
                        Name = t.Name,
                        AcademicTitleType = t.AcademicTitleType,
                        Order = t.Order
                    })
                })
            };
        }
    }
}