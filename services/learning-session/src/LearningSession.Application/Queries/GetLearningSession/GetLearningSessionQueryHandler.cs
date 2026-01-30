using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LearningSession.Application.DTOs;
using LearningSession.Application.Repositories;
using LearningSession.Domain.Entities;

namespace LearningSession.Application.Queries.GetLearningSession
{
    public class GetLearningSessionQueryHandler : IRequestHandler<GetLearningSessionQuery, LearningSessionDto>
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILearningSessionRepository _repository;

        public GetLearningSessionQueryHandler(AutoMapper.IMapper mapper, ILearningSessionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<LearningSessionDto> Handle(GetLearningSessionQuery request, CancellationToken cancellationToken)
        {
            var session = await _repository.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException("LearningSession not found");

            return _mapper.Map<LearningSessionDto>(session);
        }
    }
}

