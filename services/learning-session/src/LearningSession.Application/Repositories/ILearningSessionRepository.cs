using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningSession.Domain.Entities;

namespace LearningSession.Application.Repositories
{
    /// <summary>
    /// Repository interface for <see cref="LearningSession"/> aggregate.
    /// Placed in Application layer so handlers can depend on it and Infrastructure can implement it.
    /// </summary>
    public interface ILearningSessionRepository
    {
        Task<LearningSession?> GetByIdAsync(Guid id);
        Task AddAsync(LearningSession session);
        Task UpdateAsync(LearningSession session);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<LearningSession>> ListAsync();
    }
}

