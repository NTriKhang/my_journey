using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LearningSession.Application.DTOs;
using LearningSession.Application.Repositories;
using LearningSession.Domain.LSessions;
using Common.Application.Messaging;
using Common.Domain;
using LearningSession.Application.Abstractions.Data;

namespace LearningSession.Application.Commands.StartLearningSession
{
    public class StartLearningSessionCommandHandler(
        AutoMapper.IMapper mapper,
        ILearningSessionRepository repository,
        IUnitOfWork unitOfWork) : ICommandHandler<StartLearningSessionCommand, Guid>
    {

        public async Task<Result<Guid>> Handle(StartLearningSessionCommand request, CancellationToken cancellationToken)
        {
            LSession lSession = LSession.StartNew(request.StartedAt, request.ActivityIds);

            await repository.AddAsync(lSession);
            await unitOfWork.SaveChangesAsync();

            return Result.Success(lSession.Id);
        }
    }
}

