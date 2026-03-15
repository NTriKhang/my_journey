using System;

namespace Shared.Kernel.Domain.Events
{
    /// <summary>
    /// Base implementation for domain events providing Id and OccurredOn.
    /// </summary>
    public abstract record DomainEventBase : IDomainEvent
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;
    }
}

