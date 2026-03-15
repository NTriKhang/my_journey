using System;
using MediatR;
using LearningActivity.Application.DTOs;

namespace LearningActivity.Application.Commands.RemoveActivity
{
    public record RemoveActivityCommand(Guid ActivityId) : IRequest<Unit>;
}

