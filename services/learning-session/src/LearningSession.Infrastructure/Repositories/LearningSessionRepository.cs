using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningSession.Application.Repositories;
using LearningSession.Domain.Entities;
using LearningSession.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LearningSession.Infrastructure.Repositories
{
    public class LearningSessionRepository : ILearningSessionRepository
    {
        private readonly LearningSessionDbContext _db;

        public LearningSessionRepository(LearningSessionDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(LearningSessionEntity session)
        {
            _db.LearningSessions.Add(session);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _db.LearningSessions.FindAsync(id);
            if (entity == null) return;
            _db.LearningSessions.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<LearningSessionEntity?> GetByIdAsync(Guid id)
        {
            return await _db.LearningSessions.FindAsync(id);
        }

        public async Task<IEnumerable<LearningSessionEntity>> ListAsync()
        {
            return await _db.LearningSessions.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(LearningSessionEntity session)
        {
            _db.LearningSessions.Update(session);
            await _db.SaveChangesAsync();
        }
    }
}

