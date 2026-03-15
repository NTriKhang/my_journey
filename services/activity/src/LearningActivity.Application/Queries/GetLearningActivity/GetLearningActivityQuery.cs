using MediatR;
using LearningActivity.Application.DTOs;

namespace LearningActivity.Application.Queries.GetLearningActivity
{
    public record GetLearningActivityQuery(Guid Id) : IRequest<LearningActivityDto?>;
}

