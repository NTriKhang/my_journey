using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LearningSession.Application.DTOs;
using LearningSession.Domain.Entities;

namespace LearningSession.Application.Queries.ListLearningSessions
{
    public class ListLearningSessionsQueryHandler : IRequestHandler<ListLearningSessionsQuery, IEnumerable<LearningSessionDto>>
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILearningSessionRepository _repository;

        public ListLearningSessionsQueryHandler(AutoMapper.IMapper mapper, ILearningSessionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<LearningSessionDto>> Handle(ListLearningSessionsQuery request, CancellationToken cancellationToken)
        {
            var sessions = await _repository.ListAsync();
            var dtos = sessions.Select(s => _mapper.Map<LearningSessionDto>(s));
            return dtos;
        }
    }
}

