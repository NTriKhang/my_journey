using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LearningSession.Application.DTOs;
using LearningSession.Domain.LSessions;
using LearningSession.Application.Repositories;
using Common.Application.Messaging;
using Common.Domain;
using LearningSession.Application.Abstractions.Data;
using System.Data.Common;

namespace LearningSession.Application.Commands.AddActivityToSession
{
    sealed class AddActivityToSessionCommandHandler(
        AutoMapper.IMapper mapper,
        ILearningSessionRepository repository,
        IUnitOfWork unitOfWork) : ICommandHandler<AddActivityToSessionCommand>
    {
        public async Task<Result> Handle(AddActivityToSessionCommand request, CancellationToken cancellationToken)
        {

            LSession session = await repository.GetByIdAsync(request.SessionId) ?? throw new KeyNotFoundException("LearningSession not found");
            session.AddActivity(request.ActivityId);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

