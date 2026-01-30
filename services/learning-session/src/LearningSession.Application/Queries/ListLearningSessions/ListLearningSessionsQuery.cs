using System.Collections.Generic;
using MediatR;
using LearningSession.Application.DTOs;

namespace LearningSession.Application.Queries.ListLearningSessions
{
    public record ListLearningSessionsQuery() : IRequest<IEnumerable<LearningSessionDto>>;
}

