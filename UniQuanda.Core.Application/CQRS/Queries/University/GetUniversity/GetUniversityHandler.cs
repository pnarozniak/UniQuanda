using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.University.GetUniversity
{
    public class GetUniversityHandler : IRequestHandler<GetUniversityQuery, GetUniversityResponseDTO?>
    {
        private readonly IUniversityRepository _universityRepository;
        public GetUniversityHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }
        public async Task<GetUniversityResponseDTO?> Handle(GetUniversityQuery request, CancellationToken ct)
        {
            var university = await _universityRepository.GetUniversityByIdAsync(request.Id, ct);
            if (university == null) return null;
            return new GetUniversityResponseDTO()
            {
                Id = university.Id,
                Name = university.Name,
                Logo = university.Logo,
                Contact = university.Contact
            };
        }
    }
}
