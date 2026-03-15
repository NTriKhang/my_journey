using System;
using System.Collections.Generic;

namespace LearningActivity.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for LearningActivityEntity.
    /// </summary>
    public sealed class LearningActivityDto
    {
        public Guid Id { get; init; }
        public Guid SessionId { get; init; }
        public string Type { get; init; } = string.Empty;
        public DateTimeOffset StartedAt { get; init; }
        public DateTimeOffset? EndedAt { get; init; }
        public TimeSpan? Duration { get; init; }
        public IReadOnlyList<Guid> MaterialIds { get; init; } = Array.Empty<Guid>();
    }
}

