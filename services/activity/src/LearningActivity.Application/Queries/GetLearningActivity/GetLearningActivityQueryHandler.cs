using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using LearningActivity.Application.DTOs;
using LearningActivity.Application.Repositories;

namespace LearningActivity.Application.Queries.GetLearningActivity
{
    public class GetLearningActivityQueryHandler : IRequestHandler<GetLearningActivityQuery, LearningActivityDto?>
    {
        private readonly ILearningActivityRepository _repository;
        private readonly IMapper _mapper;

        public GetLearningActivityQueryHandler(ILearningActivityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LearningActivityDto?> Handle(GetLearningActivityQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return null;
            return _mapper.Map<LearningActivityDto>(entity);
        }
    }
}

