using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningActivity.Domain.Entities
{
    /// <summary>
    /// Represents a single learning activity that belongs to a learning session.
    /// - Activities belong to a session
    /// - Activity types are extensible (stored as string)
    /// - Activities may reference multiple materials (by id)
    /// </summary>
    public class LearningActivityEntity
    {
        public Guid Id { get; private set; }

        /// <summary>
        /// The owning LearningSession id. Activities belong to exactly one session.
        /// </summary>
        public Guid SessionId { get; private set; }

        /// <summary>
        /// Extensible activity type (e.g. "listening", "reading", "exercise").
        /// Stored as string to allow easy extension without code changes.
        /// </summary>
        public string Type { get; private set; } = string.Empty;

        public DateTimeOffset StartedAt { get; private set; }
        public DateTimeOffset? EndedAt { get; private set; }

        /// <summary>
        /// Computed duration when activity is ended.
        /// </summary>
        public TimeSpan? Duration => EndedAt.HasValue ? EndedAt - StartedAt : null;

        private readonly List<Guid> _materialIds = new();
        public IReadOnlyList<Guid> MaterialIds => _materialIds.AsReadOnly();

        // For ORM / serializer
        private LearningActivityEntity() { }

        /// <summary>
        /// Factory to create a new LearningActivityEntity.
        /// Ensures required values are provided and generates an Id if missing.
        /// </summary>
        public static LearningActivityEntity StartNew(Guid? id, Guid sessionId, string type, DateTimeOffset startedAt, IEnumerable<Guid>? materialIds = null)
        {
            if (sessionId == Guid.Empty) throw new ArgumentException("SessionId must be provided", nameof(sessionId));
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentException("Type must be provided", nameof(type));
            if (startedAt == default) throw new ArgumentException("StartedAt must be provided", nameof(startedAt));

            var activity = new LearningActivityEntity
            {
                Id = id == null || id == Guid.Empty ? Guid.NewGuid() : id.Value,
                SessionId = sessionId,
                Type = type.Trim(),
                StartedAt = startedAt
            };

            if (materialIds != null)
                activity._materialIds.AddRange(materialIds.Where(g => g != Guid.Empty));

            return activity;
        }

        /// <summary>
        /// Ends the activity. Can only be called once.
        /// </summary>
        public void End(DateTimeOffset endedAt)
        {
            if (EndedAt != null) throw new InvalidOperationException("Activity has already been ended.");
            if (endedAt < StartedAt) throw new ArgumentException("EndedAt cannot be before StartedAt", nameof(endedAt));

            EndedAt = endedAt;
        }

        public void AddMaterial(Guid materialId)
        {
            if (materialId == Guid.Empty) throw new ArgumentException(nameof(materialId));
            if (!_materialIds.Contains(materialId))
                _materialIds.Add(materialId);
        }

        public bool RemoveMaterial(Guid materialId) => _materialIds.Remove(materialId);
    }
}
