using LearningSession.Application.DTOs;
using Common.Application.Messaging;

namespace LearningSession.Application.Commands.StartLearningSession
{
    public record StartLearningSessionCommand(Guid? Id, DateTimeOffset StartedAt, IEnumerable<Guid>? ActivityIds = null) : ICommand<Guid>;
}

