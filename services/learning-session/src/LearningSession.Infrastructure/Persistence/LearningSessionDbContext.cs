using LearningSession.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningSession.Infrastructure.Persistence
{
    /// <summary>
    /// EF Core DbContext for LearningSession service.
    /// The domain keeps the activity ids as an internal collection; we do not map it to a dedicated column here.
    /// </summary>
    public class LearningSessionDbContext : DbContext
    {
        public LearningSessionDbContext(DbContextOptions<LearningSessionDbContext> options) : base(options)
        {
        }

        public DbSet<LearningSessionEntity> LearningSessions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Learning");

            modelBuilder.Entity<LearningSessionEntity>(b =>
            {
                b.ToTable("LearningSessions");
                b.HasKey(e => e.Id);

                b.Property(e => e.StartedAt).IsRequired();
                b.Property(e => e.EndedAt);

                // store enum as string
                b.Property(e => e.Status)
                    .HasConversion<string>()
                    .IsRequired();

                // Do not map the in-memory activity id collection to a column.
                b.Ignore(e => e.LearningActivityIds);
            });
        }
    }
}

