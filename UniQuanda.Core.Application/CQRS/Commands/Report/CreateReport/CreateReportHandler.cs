using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Commands.Report.CreateReport
{
    public class CreateReportHandler : IRequestHandler<CreateReportCommand, bool>
    {
        private readonly IReportRepository _reportRepository;
        public CreateReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<bool> Handle(CreateReportCommand request, CancellationToken ct)
        {
            var isCreated = await this._reportRepository.CreateReportAsync(request, ct);
            return isCreated;
        }
    }
}