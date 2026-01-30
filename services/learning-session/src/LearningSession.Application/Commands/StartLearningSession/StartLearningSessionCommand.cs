using System;
using System.Collections.Generic;
using MediatR;
using LearningSession.Application.DTOs;

namespace LearningSession.Application.Commands.StartLearningSession
{
    public record StartLearningSessionCommand(Guid? Id, DateTimeOffset StartedAt, IEnumerable<Guid>? ActivityIds = null)
        : IRequest<LearningSessionDto>;
}

