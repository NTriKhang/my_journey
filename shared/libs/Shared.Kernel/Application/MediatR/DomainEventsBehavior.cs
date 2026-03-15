using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shared.Kernel.Application.Outbox;
using Shared.Kernel.Domain.Events;

namespace Shared.Kernel.Application.MediatR
{
    /// <summary>
    /// MediatR pipeline behavior that finds domain events on AggregateRoot instances
    /// and materializes OutboxMessage entries via IOutboxRepository.
    /// </summary>
    /// <typeparam name="TRequest">MediatR request type</typeparam>
    /// <typeparam name="TResponse">MediatR response type</typeparam>
    public class DomainEventsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IOutboxRepository _outboxRepository;

        public DomainEventsBehavior(IOutboxRepository outboxRepository)
        {
            _outboxRepository = outboxRepository ?? throw new ArgumentNullException(nameof(outboxRepository));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Execute handler first so aggregates may have been modified and events recorded
            var response = await next();

            try
            {
                // Inspect request and response for aggregate roots carrying domain events.
                // Common pattern: commands carry an Aggregate or handler returns a modified Aggregate.
                var aggregates = new object[] { request, response }
                    .Where(x => x is not null)
                    .SelectMany(x => x.GetType().GetProperties().Select(p => p.GetValue(x)))
                    .Where(v => v is not null)
                    .Distinct()
                    .ToArray();

                foreach (var agg in aggregates)
                {
                    if (agg is null) continue;

                    var eventCollectionProp = agg.GetType().GetProperty("DomainEvents");
                    if (eventCollectionProp is null) continue;

                    if (eventCollectionProp.GetValue(agg) is System.Collections.IEnumerable events)
                    {
                        foreach (var ev in events)
                        {
                            if (ev is not IDomainEvent domainEvent) continue;

                            var payload = JsonSerializer.Serialize(ev, ev.GetType());
                            var message = new OutboxMessage
                            {
                                Type = ev.GetType().Name,
                                Content = payload,
                                OccurredOn = domainEvent.OccurredOn,
                                Headers = null
                            };

                            await _outboxRepository.AddAsync(message, cancellationToken);
                        }
                    }
                }
            }
            catch
            {
                // Swallow exceptions from outbox creation to avoid impacting primary flow.
                // Implementations may prefer to surface or log these errors.
            }

            return response;
        }
    }
}

