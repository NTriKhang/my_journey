using System;
using System.Collections.Generic;

namespace Shared.Kernel.Application.Outbox
{
    /// <summary>
    /// Represents an outbox message serialized and persisted by a service.
    /// Services persist instances of this model in their own infrastructure.
    /// </summary>
    public sealed class OutboxMessage
    {
        /// <summary>
        /// Unique identifier for the outbox message.
        /// </summary>
        public Guid Id { get; init; } = Guid.NewGuid();

        /// <summary>
        /// CLR type name or a transport-friendly event type (e.g. 'ActivityStarted').
        /// </summary>
        public string Type { get; init; } = string.Empty;

        /// <summary>
        /// Serialized event payload (JSON recommended).
        /// </summary>
        public string Content { get; init; } = string.Empty;

        /// <summary>
        /// UTC time when the original domain event occurred.
        /// </summary>
        public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// If the message has been processed/published, this is the UTC time it was processed.
        /// </summary>
        public DateTimeOffset? ProcessedOn { get; set; }

        /// <summary>
        /// Number of attempts to publish/process this message.
        /// </summary>
        public int Attempts { get; set; }

        /// <summary>
        /// Optional transport headers and metadata.
        /// </summary>
        public IReadOnlyDictionary<string, string>? Headers { get; init; }
    }
}

