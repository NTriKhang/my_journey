using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LearningActivity.Application.Repositories;

namespace LearningActivity.Application.Commands.RemoveActivity
{
    public class RemoveActivityCommandHandler : IRequestHandler<RemoveActivityCommand, Unit>
    {
        private readonly ILearningActivityRepository _repository;

        public RemoveActivityCommandHandler(ILearningActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RemoveActivityCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.ActivityId);
            return Unit.Value;
        }
    }
}

