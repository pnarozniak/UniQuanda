using MediatR;
using UniQuanda.Core.Application.CQRS.Queries.Auth.GetReportTypes;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;

namespace UniQuanda.Core.Application.CQRS.Queries.Ranking.GetTop5Users
{
    public class GetTop5UsersHandler : IRequestHandler<GetTop5UsersQuery, GetTop5UsersResponseDTO>
    {
        private readonly IRankingRepository _rankingRepository;

        public GetTop5UsersHandler(IRankingRepository rankingRepository, ICacheService cacheService)
        {
            _rankingRepository = rankingRepository;
        }

        public async Task<GetTop5UsersResponseDTO> Handle(GetTop5UsersQuery request, CancellationToken ct)
        {
            var top5Users = await _rankingRepository.GetTop5UsersFromCacheAsync(ct);
            if (top5Users is null)
            {
                top5Users = await _rankingRepository.GetTop5UsersAsync(ct);
                await _rankingRepository.SaveTop5UsersToCacheAsync(top5Users, ct);
            }
            return new GetTop5UsersResponseDTO()
            {
                Top5Users = top5Users.Select(u => new AppUserInRankingResponseDTO
                {
                    Id = u.Id,
                    Nickname = u.Nickname,
                    Avatar = u.Avatar,
                    Points = u.Points ?? 0
                })
            };
        }
    }
}