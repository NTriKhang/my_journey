using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LearningSession.Application.DTOs;
using LearningSession.Domain.Entities;
using LearningSession.Application.Repositories;

namespace LearningSession.Application.Commands.StartLearningSession
{
    public class StartLearningSessionCommandHandler : IRequestHandler<StartLearningSessionCommand, LearningSessionDto>
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILearningSessionRepository _repository;

        public StartLearningSessionCommandHandler(AutoMapper.IMapper mapper, ILearningSessionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<LearningSessionDto> Handle(StartLearningSessionCommand request, CancellationToken cancellationToken)
        {
            var session = LearningSessionEntity.StartNew(request.Id, request.StartedAt, request.ActivityIds);

            // persist session via repository
            await _repository.AddAsync(session);

            return _mapper.Map<LearningSessionDto>(session);
        }
    }
}

