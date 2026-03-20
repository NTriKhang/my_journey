using ICommand = Common.Application.Messaging.ICommand;

namespace LearningSession.Application.Commands.AddActivityToSession
{
    public record AddActivityToSessionCommand(Guid SessionId, Guid ActivityId) : ICommand;
}

