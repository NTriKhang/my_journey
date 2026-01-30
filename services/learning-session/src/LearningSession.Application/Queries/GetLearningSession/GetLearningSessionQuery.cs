using System;
using MediatR;
using LearningSession.Application.DTOs;

namespace LearningSession.Application.Queries.GetLearningSession
{
    public record GetLearningSessionQuery(Guid Id) : IRequest<LearningSessionDto>;
}

