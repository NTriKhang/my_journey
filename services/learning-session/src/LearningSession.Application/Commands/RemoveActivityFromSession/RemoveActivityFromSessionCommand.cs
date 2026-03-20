using Common.Application.Messaging;

namespace LearningSession.Application.Commands.RemoveActivityFromSession
{
    public record RemoveActivityFromSessionCommand(Guid SessionId, Guid ActivityId) : ICommand;
}