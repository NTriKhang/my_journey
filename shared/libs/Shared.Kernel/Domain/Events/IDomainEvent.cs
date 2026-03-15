using System;

namespace Shared.Kernel.Domain.Events
{
    /// <summary>
    /// Contract for domain events raised by aggregates.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Unique identifier for the domain event instance.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// UTC date/time when the domain event occurred.
        /// </summary>
        DateTimeOffset OccurredOn { get; }
    }
}
