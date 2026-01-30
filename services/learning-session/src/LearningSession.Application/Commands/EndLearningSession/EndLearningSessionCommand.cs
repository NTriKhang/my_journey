using System;
using MediatR;
using LearningSession.Application.DTOs;

namespace LearningSession.Application.Commands.EndLearningSession
{
    public record EndLearningSessionCommand(Guid Id, DateTimeOffset EndedAt) : IRequest<LearningSessionDto>;
}

