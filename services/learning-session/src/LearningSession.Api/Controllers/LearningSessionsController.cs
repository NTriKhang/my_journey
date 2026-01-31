using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LearningSession.Application.DTOs;
using LearningSession.Application.Commands.StartLearningSession;
using LearningSession.Application.Commands.EndLearningSession;
using LearningSession.Application.Commands.AddActivityToSession;
using LearningSession.Application.Commands.RemoveActivityFromSession;
using LearningSession.Application.Queries.ListLearningSessions;
using LearningSession.Application.Queries.GetLearningSession;

namespace LearningSession.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LearningSessionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LearningSessionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Start a new learning session.
        /// POST /api/learningsessions
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<LearningSessionDto>> Start([FromBody] StartRequest request)
        {
            var cmd = new StartLearningSessionCommand(request.Id, request.StartedAt, request.ActivityIds);
            var result = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// List all learning sessions.
        /// GET /api/learningsessions
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LearningSessionDto>>> List()
        {
            var result = await _mediator.Send(new ListLearningSessionsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get a single learning session by id.
        /// GET /api/learningsessions/{id}
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LearningSessionDto>> Get(Guid id)
        {
            var result = await _mediator.Send(new GetLearningSessionQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// End an existing learning session.
        /// PUT /api/learningsessions/{id}/end
        /// </summary>
        [HttpPut("{id:guid}/end")]
        public async Task<ActionResult<LearningSessionDto>> End(Guid id, [FromBody] EndRequest request)
        {
            var cmd = new EndLearningSessionCommand(id, request.EndedAt);
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        /// <summary>
        /// Add an activity to a session.
        /// POST /api/learningsessions/{id}/activities
        /// </summary>
        [HttpPost("{id:guid}/activities")]
        public async Task<ActionResult<LearningSessionDto>> AddActivity(Guid id, [FromBody] ActivityRequest request)
        {
            var cmd = new AddActivityToSessionCommand(id, request.ActivityId);
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        /// <summary>
        /// Remove an activity from a session.
        /// DELETE /api/learningsessions/{id}/activities/{activityId}
        /// </summary>
        [HttpDelete("{id:guid}/activities/{activityId:guid}")]
        public async Task<ActionResult<LearningSessionDto>> RemoveActivity(Guid id, Guid activityId)
        {
            var cmd = new RemoveActivityFromSessionCommand(id, activityId);
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        // Request DTOs used for binding
        public record StartRequest(Guid? Id, DateTimeOffset StartedAt, IEnumerable<Guid>? ActivityIds = null);
        public record EndRequest(DateTimeOffset EndedAt);
        public record ActivityRequest(Guid ActivityId);
    }
}

