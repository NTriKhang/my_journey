using System;
using System.Collections.Generic;

namespace Shared.Kernel.Integration
{
    /// <summary>
    /// Optional integration event contract used when domain events are translated to cross-service messages.
    /// </summary>
    public interface IIntegrationEvent
    {
        /// <summary>
        /// Unique identifier for the integration event.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// UTC time when the event occurred.
        /// </summary>
        DateTimeOffset OccurredOn { get; }

        /// <summary>
        /// Optional headers/metadata for transports.
        /// </summary>
        IReadOnlyDictionary<string, string>? Headers { get; }
    }
}

