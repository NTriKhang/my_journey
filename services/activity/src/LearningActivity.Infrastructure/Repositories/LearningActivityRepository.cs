using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningActivity.Application.Repositories;
using LearningActivity.Domain.Entities;
using LearningActivity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LearningActivity.Infrastructure.Repositories
{
    public class LearningActivityRepository : ILearningActivityRepository
    {
        private readonly LearningActivityDbContext _db;

        public LearningActivityRepository(LearningActivityDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(LearningActivityEntity activity)
        {
            _db.LearningActivities.Add(activity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _db.LearningActivities.FindAsync(id);
            if (entity == null) return;
            _db.LearningActivities.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<LearningActivityEntity?> GetByIdAsync(Guid id)
        {
            return await _db.LearningActivities.FindAsync(id);
        }

        public async Task<IEnumerable<LearningActivityEntity>> ListBySessionAsync(Guid sessionId)
        {
            return await _db.LearningActivities.AsNoTracking().Where(a => a.SessionId == sessionId).ToListAsync();
        }

        public async Task UpdateAsync(LearningActivityEntity activity)
        {
            _db.LearningActivities.Update(activity);
            await _db.SaveChangesAsync();
        }
    }
}

