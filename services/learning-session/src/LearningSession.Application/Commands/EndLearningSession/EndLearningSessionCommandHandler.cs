using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LearningSession.Application.DTOs;
using LearningSession.Domain.Entities;
using LearningSession.Application.Repositories;

namespace LearningSession.Application.Commands.EndLearningSession
{
    public class EndLearningSessionCommandHandler : IRequestHandler<EndLearningSessionCommand, LearningSessionDto>
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILearningSessionRepository _repository;

        public EndLearningSessionCommandHandler(AutoMapper.IMapper mapper, ILearningSessionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<LearningSessionDto> Handle(EndLearningSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException("LearningSession not found");

            session.End(request.EndedAt);

            await _repository.UpdateAsync(session);

            return _mapper.Map<LearningSessionDto>(session);
        }
    }
}

