using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.University.GetUniversity
{
    public class GetUniversityRequestDTO
    {
        [Required]
        public int Id { get; set; }
    }

    public class GetUniversityResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Contact { get; set; }
    }
}
