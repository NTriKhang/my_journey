# Shared.Kernel Integration Plan

This document captures the step-by-step plan to integrate `Shared.Kernel` into the LearningSession service. It mirrors the actionable todo list and maps each change to the repository files that will be edited.

## Goal
Enable domain event collection (AggregateRoot) and transactional Outbox materialization in the LearningSession service by using `shared/libs/Shared.Kernel` primitives and pipeline behavior.

## High-level mapping
- Domain primitives (AggregateRoot, IDomainEvent): [`shared/libs/Shared.Kernel/Domain/AggregateRoot.cs`](shared/libs/Shared.Kernel/Domain/AggregateRoot.cs:1), [`shared/libs/Shared.Kernel/Domain/Events/IDomainEvent.cs`](shared/libs/Shared.Kernel/Domain/Events/IDomainEvent.cs:1)
- Outbox model + contract: [`shared/libs/Shared.Kernel/Application/Outbox/OutboxMessage.cs`](shared/libs/Shared.Kernel/Application/Outbox/OutboxMessage.cs:1), [`shared/libs/Shared.Kernel/Application/Outbox/IOutboxRepository.cs`](shared/libs/Shared.Kernel/Application/Outbox/IOutboxRepository.cs:1)
- Pipeline behavior: [`shared/libs/Shared.Kernel/Application/MediatR/DomainEventsBehavior.cs`](shared/libs/Shared.Kernel/Application/MediatR/DomainEventsBehavior.cs:1)

## Step-by-step plan

1. Add project reference from Application to Shared.Kernel
   - File to edit: [`services/learning-session/src/LearningSession.Application/LearningSession.Application.csproj`](services/learning-session/src/LearningSession.Application/LearningSession.Application.csproj:1)
   - Change: add a <ProjectReference> to the `shared/libs/Shared.Kernel` project so Application code can consume domain primitives and the pipeline behavior.

2. Make domain entity derive from AggregateRoot and raise events
   - File to edit: [`services/learning-session/src/LearningSession.Domain/Entities/LearningSessionEntity.cs`](services/learning-session/src/LearningSession.Domain/Entities/LearningSessionEntity.cs:1)
   - Change: inherit from [`shared/libs/Shared.Kernel/Domain/AggregateRoot.cs`](shared/libs/Shared.Kernel/Domain/AggregateRoot.cs:1) and call `AddDomainEvent(...)` at meaningful state changes (e.g., when activity is added or session ended).

3. Add domain event types in the Domain layer
   - New files: e.g. [`services/learning-session/src/LearningSession.Domain/Events/ActivityAdded.cs`](services/learning-session/src/LearningSession.Domain/Events/ActivityAdded.cs:1) and [`services/learning-session/src/LearningSession.Domain/Events/SessionEnded.cs`](services/learning-session/src/LearningSession.Domain/Events/SessionEnded.cs:1)
   - Implement `IDomainEvent` from `Shared.Kernel` for these event types.

4. Implement Outbox repository in Infrastructure
   - New file: [`services/learning-session/src/LearningSession.Infrastructure/Outbox/LearningSessionOutboxRepository.cs`](services/learning-session/src/LearningSession.Infrastructure/Outbox/LearningSessionOutboxRepository.cs:1)
   - Implement interface: [`shared/libs/Shared.Kernel/Application/Outbox/IOutboxRepository.cs`](shared/libs/Shared.Kernel/Application/Outbox/IOutboxRepository.cs:1)
   - Persist `OutboxMessage` instances to a service-local Outbox table: [`shared/libs/Shared.Kernel/Application/Outbox/OutboxMessage.cs`](shared/libs/Shared.Kernel/Application/Outbox/OutboxMessage.cs:1)

5. Add Outbox DbSet and mapping in DbContext
   - File to edit: [`services/learning-session/src/LearningSession.Infrastructure/Persistence/LearningSessionDbContext.cs`](services/learning-session/src/LearningSession.Infrastructure/Persistence/LearningSessionDbContext.cs:1)
   - Change: add `public DbSet<OutboxMessage> Outbox { get; set; }` and configure table mapping.

6. Create EF Core migration for Outbox table
   - Add migration under: [`services/learning-session/src/LearningSession.Infrastructure/Migrations`](services/learning-session/src/LearningSession.Infrastructure/Migrations:1)

7. Ensure transactional atomicity
   - Ensure aggregate state changes and Outbox inserts are persisted in the same `DbContext.SaveChangesAsync()` call (use the same scoped `LearningSessionDbContext` instance in repositories).

8. Register DI for Outbox repo and pipeline behavior
   - File to edit: [`services/learning-session/src/LearningSession.Infrastructure/ServiceCollectionExtensions.cs`](services/learning-session/src/LearningSession.Infrastructure/ServiceCollectionExtensions.cs:1)
   - Add registrations:
     - `services.AddScoped<Shared.Kernel.Application.Outbox.IOutboxRepository, LearningSession.Infrastructure.Outbox.LearningSessionOutboxRepository>();`
     - `services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(Shared.Kernel.Application.MediatR.DomainEventsBehavior<,>));`

9. Add tests
   - Unit test the pipeline behavior: mock `IOutboxRepository` and assert `AddAsync` is invoked when aggregates contain domain events.
   - Integration test: ensure saving an aggregate persists both domain data and Outbox rows in same transaction.

10. Documentation
    - Update [`shared/libs/Shared.Kernel/USAGE.md`](shared/libs/Shared.Kernel/USAGE.md:1) with any service-specific notes.
    - Update service README: [`services/learning-session/README.md`](services/learning-session/README.md:1)

11. Optional: background Outbox processor
    - Implement a worker in Infrastructure to scan the Outbox table, publish to a broker, and mark messages processed.

## Notes & recommendations
- Use a stable JSON serializer (System.Text.Json with configured options or Newtonsoft.Json) when the pipeline serializes domain events (see [`shared/libs/Shared.Kernel/Application/MediatR/DomainEventsBehavior.cs`](shared/libs/Shared.Kernel/Application/MediatR/DomainEventsBehavior.cs:1)).
- Keep Outbox schema owned by the service — shared kernel intentionally does not provide concrete persistence.
- Add logging around outbox creation and publishing to monitor processing failures.

---

## Checklist (copy for work tracking)

- [ ] Add ProjectReference to `shared/libs/Shared.Kernel`
- [ ] Modify `LearningSessionEntity` to inherit `AggregateRoot` and raise domain events
- [ ] Add domain event types (ActivityAdded, SessionEnded)
- [ ] Implement `LearningSessionOutboxRepository`
- [ ] Add DbSet<OutboxMessage> and mapping in `LearningSessionDbContext`
- [ ] Create and apply EF Core migration for Outbox table
- [ ] Ensure transactional save semantics
- [ ] Register DI for Outbox repository and DomainEventsBehavior
- [ ] Add unit and integration tests
- [ ] Update documentation

