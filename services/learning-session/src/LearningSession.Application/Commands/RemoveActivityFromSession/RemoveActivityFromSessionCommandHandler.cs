using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LearningSession.Application.DTOs;
using LearningSession.Domain.Entities;
using LearningSession.Application.Repositories;

namespace LearningSession.Application.Commands.RemoveActivityFromSession
{
    public class RemoveActivityFromSessionCommandHandler : IRequestHandler<RemoveActivityFromSessionCommand, LearningSessionDto>
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILearningSessionRepository _repository;

        public RemoveActivityFromSessionCommandHandler(AutoMapper.IMapper mapper, ILearningSessionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<LearningSessionDto> Handle(RemoveActivityFromSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _repository.GetByIdAsync(request.SessionId) ?? throw new KeyNotFoundException("LearningSession not found");

            session.RemoveActivity(request.ActivityId);

            await _repository.UpdateAsync(session);

            return _mapper.Map<LearningSessionDto>(session);
        }
    }
}

