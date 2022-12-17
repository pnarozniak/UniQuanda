using MediatR;
using Microsoft.AspNetCore.Http;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Settings.AddTitleRequest
{
    public class AddTitleRequestCommand : IRequest<bool>
    {
        public AddTitleRequestCommand(AddTitleRequestRequestDTO request, int uid)
        {
            CreatedAt = DateTime.Now;
            AcademicTitleId = request.AcademicTitleId;
            Scan = request.Scan;
            AdditionalInfo = request.AdditionalInfo;
            UserId = uid;
        }
        public DateTime CreatedAt { get; set; }
        public int AcademicTitleId { get; set; }
        public IFormFile Scan { get; set; }
        public string? AdditionalInfo { get; set; }
        public int UserId { get; set; }
    }
}
