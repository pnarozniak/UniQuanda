using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Commands.Premium.CreatePremiumPayment;
using UniQuanda.Core.Application.CQRS.Commands.Premium.HandlePremiumPaymentStatus;
using UniQuanda.Core.Application.CQRS.Queries.Premium.GetPremiumPayments;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Infrastructure.Enums;
using UniQuanda.Presentation.API.Attributes;
using UniQuanda.Presentation.API.Extensions;

namespace UniQuanda.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = JwtTokenRole.User)]
    public class PremiumController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PremiumController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Recaptcha]
        [HttpPost("create-payment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreatePremiumPaymentResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(CreatePremiumPaymentResponseDTO))]
        public async Task<IActionResult> CreatePremiumPayment([FromBody] CreatePremiumPaymentRequestDTO request, CancellationToken ct)
        {
            var command = new CreatePremiumPaymentCommand(request, User.GetId()!.Value);
            var result = await _mediator.Send(command, ct);
            return result.Status switch
            {
                CreatePremiumPaymentResultEnum.Successful => Ok(result),
                CreatePremiumPaymentResultEnum.ContentNotExist => NotFound(),
                _ => Conflict(result)
            };
        }

        [Recaptcha]
        [HttpPost("update-status")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HandlePremiumPaymentStatusResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(HandlePremiumPaymentStatusResponseDTO))]
        public async Task<IActionResult> HandlePremiumPayment(CancellationToken ct)
        {
            var command = new HandlePremiumPaymentStatusCommand(User.GetId()!.Value);
            var result = await _mediator.Send(command, ct);
            return result.Status switch
            {
                HandlePremiumPaymentStatusResultEnum.Successful => Ok(result),
                HandlePremiumPaymentStatusResultEnum.ContentNotExist => NotFound(),
                _ => Conflict(result)
            };
        }

        [Recaptcha]
        [HttpGet("payments-info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPremiumPaymentsResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPremiumPayments([FromQuery] GetPremiumPaymentsRequestDTO request, CancellationToken ct)
        {
            var query = new GetPremiumPaymentsQuery(request, User.GetId()!.Value);
            var result = await _mediator.Send(query, ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}