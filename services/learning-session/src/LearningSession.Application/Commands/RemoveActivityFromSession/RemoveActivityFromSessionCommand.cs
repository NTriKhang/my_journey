using System;
using MediatR;
using LearningSession.Application.DTOs;

namespace LearningSession.Application.Commands.RemoveActivityFromSession
{
    public record RemoveActivityFromSessionCommand(Guid SessionId, Guid ActivityId) : IRequest<LearningSessionDto>;
}

