using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningSession.Domain.Entities;

namespace LearningSession.Application.Repositories
{
    /// <summary>
    /// Repository interface for <see cref="LearningSessionEntity"/> aggregate.
    /// Placed in Application layer so handlers can depend on it and Infrastructure can implement it.
    /// </summary>
    public interface ILearningSessionRepository
    {
        Task<LearningSessionEntity?> GetByIdAsync(Guid id);
        Task AddAsync(LearningSessionEntity session);
        Task UpdateAsync(LearningSessionEntity session);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<LearningSessionEntity>> ListAsync();
    }
}

