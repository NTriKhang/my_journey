using System;
using MediatR;
using LearningSession.Application.DTOs;

namespace LearningSession.Application.Commands.AddActivityToSession
{
    public record AddActivityToSessionCommand(Guid SessionId, Guid ActivityId) : IRequest<LearningSessionDto>;
}

