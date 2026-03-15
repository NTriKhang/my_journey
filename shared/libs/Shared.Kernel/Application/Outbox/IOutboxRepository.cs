using System.Threading;
using System.Threading.Tasks;

namespace Shared.Kernel.Application.Outbox
{
    /// <summary>
    /// Interface to persist outbox messages within a service's infrastructure layer.
    /// Implementations should ensure messages can be saved within the same transaction as aggregate persistence.
    /// </summary>
    public interface IOutboxRepository
    {
        /// <summary>
        /// Adds an outbox message to the persistent store.
        /// </summary>
        Task AddAsync(OutboxMessage message, CancellationToken cancellationToken = default);
    }
}

