using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetRequestedTitles
{
    public class GetRequestedTitlesRequestDTO
    {
        [Required]
        public int UserId { get; set; }
    }

    public class GetRequestedTitlesResponseDTO
    {
        public int RequestId { get; set; }
        public string Title { get; set; }
        public DateTime RequestDate { get; set; }
        public TitleRequestStatusEnum Status { get; set; }
    }
}
