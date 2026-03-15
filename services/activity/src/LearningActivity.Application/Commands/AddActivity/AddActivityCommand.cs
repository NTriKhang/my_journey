using System;
using System.Collections.Generic;
using MediatR;
using LearningActivity.Application.DTOs;

namespace LearningActivity.Application.Commands.AddActivity
{
    // Create a new LearningActivity
    public record AddActivityCommand(Guid? Id, Guid SessionId, string Type, DateTimeOffset StartedAt, IEnumerable<Guid>? MaterialIds = null)
        : IRequest<LearningActivityDto>;
}

