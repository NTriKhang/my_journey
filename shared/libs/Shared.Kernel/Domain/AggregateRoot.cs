using System.Collections.Generic;
using Shared.Kernel.Domain.Events;

namespace Shared.Kernel.Domain
{
    /// <summary>
    /// Base aggregate root that collects domain events raised by the aggregate.
    /// </summary>
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// Read-only collection of domain events that have been added to this aggregate.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Adds a domain event to the aggregate's collection.
        /// </summary>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent is null) return;
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a domain event from the collection if present.
        /// </summary>
        protected void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent is null) return;
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Clears all domain events from the aggregate.
        /// </summary>
        protected void ClearDomainEvents() => _domainEvents.Clear();
    }
}

