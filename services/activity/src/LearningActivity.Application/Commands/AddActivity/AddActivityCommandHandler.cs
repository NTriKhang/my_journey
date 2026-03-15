using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LearningActivity.Application.DTOs;
using LearningActivity.Domain.Entities;
using LearningActivity.Application.Repositories;

namespace LearningActivity.Application.Commands.AddActivity
{
    public class AddActivityCommandHandler : IRequestHandler<AddActivityCommand, LearningActivityDto>
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILearningActivityRepository _repository;

        public AddActivityCommandHandler(AutoMapper.IMapper mapper, ILearningActivityRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<LearningActivityDto> Handle(AddActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = LearningActivityEntity.StartNew(request.Id, request.SessionId, request.Type, request.StartedAt, request.MaterialIds);

            await _repository.AddAsync(activity);

            return _mapper.Map<LearningActivityDto>(activity);
        }
    }
}

