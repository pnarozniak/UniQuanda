using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Report.CreateReport;
using UniQuanda.Core.Application.CQRS.Queries.Auth.CreateReport;
using UniQuanda.Core.Application.CQRS.Queries.Auth.GetReportTypes;
using UniQuanda.Core.Application.CQRS.Queries.Report.GetReportTypes;
using UniQuanda.Presentation.API.Attributes;
using UniQuanda.Presentation.API.Extensions;

namespace UniQuanda.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "user")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets report types from requested category
        /// </summary>
        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetReportTypesResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReportTypes(
            [FromQuery] GetReportTypesRequestDTO request,
            CancellationToken ct) 
        {
            var query = new GetReportTypesQuery(request);
            var reportTypes = await _mediator.Send(query, ct);
            return reportTypes is not null ? Ok(reportTypes) : NotFound();
        }

        /// <summary>
        ///     Creates new report
        /// </summary>
        [Recaptcha]
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateReport(
            [FromBody] CreateReportRequestDTO request,
            CancellationToken ct) 
        {
            var command = new CreateReportCommand(request, User.GetId().Value);
            var isCreated = await _mediator.Send(command, ct);
            return isCreated ? NoContent() : Conflict();
        }
    }
}