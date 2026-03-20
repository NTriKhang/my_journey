using System.Threading;
using System.Threading.Tasks;
using Common.Application.Messaging;
using Common.Domain;
using LearningSession.Application.Repositories;
using LearningSession.Domain.LSessions;
using LearningSession.Application.Abstractions.Data;

namespace LearningSession.Application.Commands.RemoveActivityFromSession
{
    public class RemoveActivityFromSessionCommandHandler(
        ILearningSessionRepository repository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveActivityFromSessionCommand>
    {
        public async Task<Result> Handle(RemoveActivityFromSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await repository.GetByIdAsync(request.SessionId);

            if (session is null)
                return Result.Failure<Guid>(LSessionErrors.NotFound(request.SessionId));

            session.RemoveActivity(request.ActivityId);

            await unitOfWork.SaveChangesAsync();

            return Result.Success(session.Id);
        }
    }
}