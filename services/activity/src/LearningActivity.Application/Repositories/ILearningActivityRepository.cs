using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningActivity.Domain.Entities;

namespace LearningActivity.Application.Repositories
{
    public interface ILearningActivityRepository
    {
        Task<LearningActivityEntity?> GetByIdAsync(Guid id);
        Task AddAsync(LearningActivityEntity activity);
        Task UpdateAsync(LearningActivityEntity activity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<LearningActivityEntity>> ListBySessionAsync(Guid sessionId);
    }
}

