using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Admin.Titles.AssignStatusToRequest;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Settings.AddTitleRequest;
using UniQuanda.Core.Application.CQRS.Commands.AppUser.Settings.ChangeTitleOrder;
using UniQuanda.Core.Application.CQRS.Queries.Admin.TitleRequest.GetAllRequests;
using UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetAllTitlesSettings;
using UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetRequestedTitles;
using UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetTitlesSettings;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Presentation.API.Extensions;

namespace UniQuanda.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicTitleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AcademicTitleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets all titles that user can request
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllTitlesSettingsResponseDTO>))]
        [HttpGet("requestable-titles")]
        public async Task<IActionResult> GetRequestableTitles(CancellationToken ct)
        {
            var query = new GetAllTitlesSettingsQuery();
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        /// <summary>
        ///     Gets all user titles with order
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetTitlesSettingsResponseDTO>))]
        [HttpGet("titles-of-user")]
        public async Task<IActionResult> GetTitlesOfUser([FromQuery] GetTitlesSettingsRequestDTO request, CancellationToken ct)
        {
            var query = new GetTitlesSettingsQuery(request);
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        /// <summary>
        ///     Gets all user requests for titles
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetRequestedTitlesResponseDTO>))]
        [Authorize(Roles = AppRole.User)]
        [HttpGet("requested-titles-of-user")]
        public async Task<IActionResult> GetRequestedTitlesOfUser(CancellationToken ct)
        {
            var request = new GetRequestedTitlesRequestDTO()
            {
                UserId = User.GetId()!.Value
            };
            var query = new GetRequestedTitlesQuery(request);
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        /// <summary>
        ///     Sets orders of user titles
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = AppRole.User)]
        [HttpPut("titles-order")]
        public async Task<IActionResult> SetOrderOfTitles([FromBody] IEnumerable<ChangeTitleOrderRequestDTO> request ,CancellationToken ct)
        {
            var command = new ChangeTitleOrderCommand(request, User.GetId()!.Value);
            var result = await _mediator.Send(command, ct);
            return result ? Ok() : BadRequest();
        }

        /// <summary>
        ///     Adds request for title
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = AppRole.User)]
        [HttpPost("add-request")]
        public async Task<IActionResult> AddRequestForTitles([FromForm] AddTitleRequestRequestDTO request,CancellationToken ct)
        {
            var command = new AddTitleRequestCommand(request, User.GetId()!.Value);
            var result = await _mediator.Send(command, ct);
            return result ? Ok() : BadRequest();
        }

        /// <summary>
        ///     Gets pending requests for titles using paging
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllRequestsResponseDTO))]
        [Authorize(Roles = AppRole.Admin)]
        [HttpGet("pending-requested-titles")]
        public async Task<IActionResult> GetRequestsForTitlesAdmin([FromQuery] GetAllRequestsRequestDTO request, CancellationToken ct)
        {
            var query = new GetAllRequestsQuery(request);
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        /// <summary>
        ///     Sets status for request for academic title. If status == Accepted then assigns academic title to user
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = AppRole.Admin)]
        [HttpPost("change-request-status")]
        public async Task<IActionResult> SetRequestForTitleStatusAdmin([FromBody] AssignStatusToRequestDTORequest request, CancellationToken ct)
        {
            var command = new AssignStatusToRequestCommand(request);
            var result = await _mediator.Send(command, ct);
            return result ? Ok() : BadRequest();
        }
    }
}
