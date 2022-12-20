using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Admin.Titles.AssignStatusToRequest
{
    public class AssignStatusToRequestDTORequest
    {
        [Required]
        public int ReuqestId { get; set; }
        [Required]
        public TitleRequestStatusEnum Status { get; set; }
    }
}

