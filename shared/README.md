# Shared (backend) — Shared Kernel guidance

This file describes the Shared Kernel artifacts and integration points for backend services under the /services folder. It captures the "Standard" scope: domain primitives, application helpers (a MediatR pipeline behavior that collects domain events), and an Outbox message model. The repository does not include a concrete DB Outbox implementation here — each service implements and persists Outbox messages in its own Infrastructure layer.

Files we plan to add under `shared/libs/Shared.Kernel` (recommended):

- `Domain/Entity.cs` — base Entity with Id
- `Domain/AggregateRoot.cs` — AggregateRoot with domain event collection and helper methods
- `Domain/Events/IDomainEvent.cs` — domain event contract
- `Integration/IIntegrationEvent.cs` — integration event contract (optional)
- `Application/Outbox/OutboxMessage.cs` — outbox message model (type, payload, timestamps)
- `Application/Outbox/IOutboxRepository.cs` — interface that services implement to persist OutboxMessage
- `Application/MediatR/DomainEventsBehavior.cs` — MediatR pipeline behavior that finds domain events and creates OutboxMessage instances

Scope chosen: Standard
- Provide domain primitives and a MediatR pipeline behavior that materializes OutboxMessage objects via `IOutboxRepository`.
- Do not provide a concrete EF Core outbox implementation in shared. Each service implements persistence so schema ownership stays local to the service.

Why this structure
- Keeps domain primitives and cross-cutting behavior consistent across services.
- Pipeline behavior centralizes the pattern of converting domain events into outbox messages so you can later implement an outbox dispatcher or event publisher consistently.

Quick usage / integration checklist (per service)

1. Add a project reference from the service Application project to the shared kernel project (after it exists):

   - Example: reference `shared/libs/Shared.Kernel` from a service Application project.

2. Implement `IOutboxRepository` in the service Infrastructure layer and register it in DI. Example registration goes into the service `ServiceCollectionExtensions` (for Activity service):

   ```csharp
   // services/activity/src/LearningActivity.Infrastructure/ServiceCollectionExtensions.cs:1
   public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
   {
       // existing registrations (DbContext, repositories...)

       // register outbox concrete implementation in this service
       services.AddScoped<Shared.Kernel.Application.Outbox.IOutboxRepository, LearningActivity.Infrastructure.Outbox.OutboxRepository>();

       // register domain events pipeline behavior
       services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(Shared.Kernel.Application.MediatR.DomainEventsBehavior<,>));
   }
   ```

   Refer to the service infra file for the Activity service: [`services/activity/src/LearningActivity.Infrastructure/ServiceCollectionExtensions.cs`](services/activity/src/LearningActivity.Infrastructure/ServiceCollectionExtensions.cs:1) and for the Session service: [`services/learning-session/src/LearningSession.Infrastructure/ServiceCollectionExtensions.cs`](services/learning-session/src/LearningSession.Infrastructure/ServiceCollectionExtensions.cs:1).

3. Raise domain events from Aggregates

   - Make domain entities that can raise events derive from `Shared.Kernel.Domain.AggregateRoot`. When something noteworthy happens, call `AddDomainEvent(new MyDomainEvent(...))`.

   - Minimal domain event example:

     ```csharp
     public sealed class ActivityStarted : Shared.Kernel.Domain.Events.IDomainEvent
     {
         public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
         public Guid ActivityId { get; }
         public Guid SessionId { get; }

         public ActivityStarted(Guid activityId, Guid sessionId)
         {
             ActivityId = activityId;
             SessionId = sessionId;
         }
     }
     
     // usage inside AggregateRoot-derived entity
     AddDomainEvent(new ActivityStarted(this.Id, this.SessionId));
     ```

4. Ensure MediatR pipeline is registered so the `DomainEventsBehavior` runs for commands/queries handled by handlers (handlers typically live in Application layer). In the host Program.cs you already register MediatR; keep that and add the pipeline behavior registration in the service infra DI (see step 2). Example host file: [`services/activity/src/LearningActivity.Api/Program.cs`](services/activity/src/LearningActivity.Api/Program.cs:1).

Notes and recommendations

- Serialization: domain events will be serialized into OutboxMessage.Content. Use a stable serializer (System.Text.Json with known options or Newtonsoft.Json) and avoid leaking non-serializable state (cyclic references). Consider adding metadata (headers) to OutboxMessage if needed.
- Transactional outbox: implement `IOutboxRepository.AddAsync` in the service to persist OutboxMessage within the same database transaction as your aggregate persistence. This ensures atomicity between state changes and outbox records.
- Outbox processing: out-of-scope for shared kernel. Services should implement a background worker that scans unprocessed outbox messages, publishes them to a message broker, and marks them processed.
- Tests: add unit tests for AggregateRoot event collection and the DomainEventsBehavior (mock IOutboxRepository) in the shared libs or in service projects.

Next steps (suggested)

- Implement the shared project skeleton under `shared/libs/Shared.Kernel` with the files listed earlier.
- Implement a concrete `IOutboxRepository` in each service's Infrastructure and register it in `ServiceCollectionExtensions`.
- Add a simple background worker in each service to publish outbox messages (optional later).
 - Implement the shared project skeleton under `shared/libs/Shared.Kernel` with the files listed earlier. A Starter implementation has been added under [`shared/libs/Shared.Kernel`](shared/libs/Shared.Kernel:1).

Files added (summary):

 - [`shared/libs/Shared.Kernel/Domain/Entity.cs`](shared/libs/Shared.Kernel/Domain/Entity.cs:1) — base Entity with Guid Id and equality helpers
 - [`shared/libs/Shared.Kernel/Domain/AggregateRoot.cs`](shared/libs/Shared.Kernel/Domain/AggregateRoot.cs:1) — AggregateRoot with domain event collection and helper methods
 - [`shared/libs/Shared.Kernel/Domain/Events/IDomainEvent.cs`](shared/libs/Shared.Kernel/Domain/Events/IDomainEvent.cs:1) — domain event contract
 - [`shared/libs/Shared.Kernel/Domain/Events/DomainEventBase.cs`](shared/libs/Shared.Kernel/Domain/Events/DomainEventBase.cs:1) — base event implementation
 - [`shared/libs/Shared.Kernel/Integration/IIntegrationEvent.cs`](shared/libs/Shared.Kernel/Integration/IIntegrationEvent.cs:1) — integration event contract
 - [`shared/libs/Shared.Kernel/Application/Outbox/OutboxMessage.cs`](shared/libs/Shared.Kernel/Application/Outbox/OutboxMessage.cs:1) — outbox message model
 - [`shared/libs/Shared.Kernel/Application/Outbox/IOutboxRepository.cs`](shared/libs/Shared.Kernel/Application/Outbox/IOutboxRepository.cs:1) — repository interface
 - [`shared/libs/Shared.Kernel/Application/MediatR/DomainEventsBehavior.cs`](shared/libs/Shared.Kernel/Application/MediatR/DomainEventsBehavior.cs:1) — MediatR pipeline behavior
 - [`shared/libs/Shared.Kernel/USAGE.md`](shared/libs/Shared.Kernel/USAGE.md:1) — quick integration instructions

Next steps for services:

 - Implement `IOutboxRepository` in each service Infra and register it along with the pipeline behavior in DI as described in [`shared/libs/Shared.Kernel/USAGE.md`](shared/libs/Shared.Kernel/USAGE.md:1).
 - Ensure the serializer used by the service is stable and consistent when serializing domain events into `OutboxMessage.Content`.


References

- Activity infra registration: [`services/activity/src/LearningActivity.Infrastructure/ServiceCollectionExtensions.cs`](services/activity/src/LearningActivity.Infrastructure/ServiceCollectionExtensions.cs:1)
- Session infra registration: [`services/learning-session/src/LearningSession.Infrastructure/ServiceCollectionExtensions.cs`](services/learning-session/src/LearningSession.Infrastructure/ServiceCollectionExtensions.cs:1)
- Example handlers that will trigger the pipeline: [`services/activity/src/LearningActivity.Application/Commands/AddActivity/AddActivityCommandHandler.cs`](services/activity/src/LearningActivity.Application/Commands/AddActivity/AddActivityCommandHandler.cs:1) and [`services/learning-session/src/LearningSession.Application/Commands/StartLearningSession/StartLearningSessionCommandHandler.cs`](services/learning-session/src/LearningSession.Application/Commands/StartLearningSession/StartLearningSessionCommandHandler.cs:1)

When you are ready I can generate the actual `shared/libs/Shared.Kernel` project skeleton and example files, or implement a concrete Outbox repository in a target service. For now this README lives at [`shared/README.md`](shared/README.md:1).

