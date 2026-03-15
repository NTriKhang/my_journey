```mermaid
flowchart LR
  Client[Client] --> Api[API controller]
  Api --> Handler[Command handler]
  Handler --> Aggregate[AggregateRoot entity]
  Aggregate -->|AddDomainEvent| DomainEventsBehavior[MediatR DomainEventsBehavior]
  DomainEventsBehavior -->|Serialize event| OutboxRepo[IOutboxRepository.AddAsync]
  OutboxRepo -->|Persist| OutboxTable[Outbox table in service DB]
  BackgroundWorker[Outbox processor] -->|Scan unprocessed| OutboxTable
  BackgroundWorker -->|Publish| Broker[Message broker / transport]
  Broker -->|Deliver| Downstream[Other services or consumers]

  subgraph DI
    RepoImpl[IOutboxRepository implementation in infra]
    DomainEventsBehavior
  end

  RepoImpl --> OutboxRepo
  Handler -->|handled within transaction| OutboxTable
```

Explanation:

- API receives request and dispatches a command to the application layer.
- Command handler applies domain changes to AggregateRoot and records domain events.
- DomainEventsBehavior materializes recorded domain events into OutboxMessage and calls IOutboxRepository.AddAsync.
- OutboxMessage records are persisted to the service-local Outbox table within the same transaction as aggregate changes.
- A BackgroundWorker scans the Outbox table, publishes messages to a message broker, and marks them processed.

