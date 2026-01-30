using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LearningSession.Application.DTOs;
using LearningSession.Domain.Entities;

namespace LearningSession.Application.Commands.AddActivityToSession
{
    public class AddActivityToSessionCommandHandler : IRequestHandler<AddActivityToSessionCommand, LearningSessionDto>
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILearningSessionRepository _repository;

        public AddActivityToSessionCommandHandler(AutoMapper.IMapper mapper, ILearningSessionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<LearningSessionDto> Handle(AddActivityToSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _repository.GetByIdAsync(request.SessionId) ?? throw new KeyNotFoundException("LearningSession not found");
            session.AddActivity(request.ActivityId);

            await _repository.UpdateAsync(session);

            return _mapper.Map<LearningSessionDto>(session);
        }
    }
}

