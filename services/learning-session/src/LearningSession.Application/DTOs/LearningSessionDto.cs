using System;
using System.Collections.Generic;
using System.Linq;
using LearningSession.Domain.Entities;
using LearningSession.Domain.ValueObjects;

namespace LearningSession.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for LearningSessionEntity entity.
    /// </summary>
    public sealed class LearningSessionDto
    {
        public Guid Id { get; init; }
        public DateTimeOffset StartedAt { get; init; }
        public DateTimeOffset? EndedAt { get; init; }
        public SessionStatus Status { get; init; }
        public IReadOnlyList<Guid> LearningActivityIds { get; init; } = Array.Empty<Guid>();

        // Mapping is handled via AutoMapper profile instead of a manual factory.
    }
}

