using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LearningActivity.Application.DTOs;
using LearningActivity.Application.Commands.AddActivity;
using LearningActivity.Application.Commands.RemoveActivity;


namespace LearningActivity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LearningActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LearningActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new learning activity.
        /// POST /api/learningactivities
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<LearningActivityDto>> Create([FromBody] CreateRequest request)
        {
            var cmd = new AddActivityCommand(request.Id, request.SessionId, request.Type, request.StartedAt, request.MaterialIds);
            var result = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// Get a learning activity by id.
        /// GET /api/learningactivities/{id}
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LearningActivityDto>> Get(Guid id)
        {
            var result = await _mediator.Send(new LearningActivity.Application.Queries.GetLearningActivity.GetLearningActivityQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Remove a learning activity.
        /// DELETE /api/learningactivities/{id}
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new RemoveActivityCommand(id));
            return NoContent();
        }

        // Request DTO used for binding
        public record CreateRequest(Guid? Id, Guid SessionId, string Type, DateTimeOffset StartedAt, IEnumerable<Guid>? MaterialIds = null);
    }
}

