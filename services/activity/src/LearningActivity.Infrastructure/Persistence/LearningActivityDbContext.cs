using LearningActivity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningActivity.Infrastructure.Persistence
{
    /// <summary>
    /// EF Core DbContext for LearningActivity service.
    /// The domain keeps the material ids as an internal collection; we map it to a separate table for persistence.
    /// </summary>
    public class LearningActivityDbContext : DbContext
    {
        public LearningActivityDbContext(DbContextOptions<LearningActivityDbContext> options) : base(options)
        {
        }

        public DbSet<LearningActivityEntity> LearningActivities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Activity");

            modelBuilder.Entity<LearningActivityEntity>(b =>
            {
                b.ToTable("LearningActivities");
                b.HasKey(e => e.Id);

                b.Property(e => e.SessionId).IsRequired();
                b.Property(e => e.Type).IsRequired();
                b.Property(e => e.StartedAt).IsRequired();
                b.Property(e => e.EndedAt);

                b.Ignore(e => e.MaterialIds);
            });
        }
    }
}

