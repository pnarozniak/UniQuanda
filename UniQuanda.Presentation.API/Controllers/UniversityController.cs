using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniQuanda.Core.Application.CQRS.Queries.University.GetUniversity;
using UniQuanda.Core.Application.CQRS.Queries.University.GetUniversityQuestions;

namespace UniQuanda.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UniversityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get university details by university id
        /// </summary>
        /// <param name="id">University Id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUniversityResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUniversity([FromRoute] int id, CancellationToken ct)
        {
            var query = new GetUniversityQuery(new GetUniversityRequestDTO() { Id = id });
            var university = await _mediator.Send(query, ct);
            return university != null ? Ok(university) : NotFound();
        }

        /// <summary>
        ///     Get university questions by university id using paging
        /// </summary>
        [HttpGet("questions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUniversityQuestionsResponseDTO))]
        public async Task<IActionResult> GetQuestions([FromQuery] GetUniversityQuestionsRequestDTO request, CancellationToken ct)
        {
            var query = new GetUniversityQuestionsQuery(request);
            return Ok(await _mediator.Send(query, ct));
        }
    }
}
