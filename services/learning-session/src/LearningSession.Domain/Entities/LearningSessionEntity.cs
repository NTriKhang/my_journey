using System;
using System.Collections.Generic;
using System.Linq;
using LearningSession.Domain.ValueObjects;

namespace LearningSession.Domain.Entities
{
    /// <summary>
    /// Represents an intentional period of learning.
    /// Rules:
    /// - Must have StartedAt
    /// - Starts in Active state
    /// - Can be Ended only once
    /// - Contains zero or more LearningActivities (stored as ids)
    /// </summary>
    public class LearningSessionEntity
    {
        public Guid Id { get; private set; }
        public DateTimeOffset StartedAt { get; private set; }
        public DateTimeOffset? EndedAt { get; private set; }
        public SessionStatus Status { get; private set; }

        private readonly List<Guid> _learningActivityIds = new();
        public IReadOnlyList<Guid> LearningActivityIds => _learningActivityIds.AsReadOnly();

        // For ORM / serializer
        private LearningSessionEntity() { }

        /// <summary>
        /// Factory to create a new LearningSessionEntity. Ensures StartedAt is provided and session starts Active.
        /// </summary>
        public static LearningSessionEntity StartNew(Guid? id, DateTimeOffset startedAt, IEnumerable<Guid>? activityIds = null)
        {
            if (startedAt == default) throw new ArgumentException("StartedAt must be provided", nameof(startedAt));

            var session = new LearningSessionEntity
            {
                Id = id == null || id == Guid.Empty ? Guid.NewGuid() : id.Value,
                StartedAt = startedAt,
                Status = SessionStatus.Active
            };

            if (activityIds != null)
                session._learningActivityIds.AddRange(activityIds);

            return session;
        }

        /// <summary>
        /// Ends the session. Can only be called once.
        /// </summary>
        public void End(DateTimeOffset endedAt)
        {
            if (EndedAt != null) throw new InvalidOperationException("Session has already been ended.");
            if (endedAt < StartedAt) throw new ArgumentException("EndedAt cannot be before StartedAt", nameof(endedAt));

            EndedAt = endedAt;
            Status = SessionStatus.Completed;
        }

        public void AddActivity(Guid activityId)
        {
            if (activityId == Guid.Empty) throw new ArgumentException(nameof(activityId));
            if (!_learningActivityIds.Contains(activityId))
                _learningActivityIds.Add(activityId);
        }

        public bool RemoveActivity(Guid activityId) => _learningActivityIds.Remove(activityId);
    }
}
