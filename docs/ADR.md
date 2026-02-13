# Architecture Decision Records (ADR)

This document records key architectural decisions for the **Japanese Learning Platform**.
All ADRs are **living documents** and may be revised as the system evolves.

Status legend:

* **Accepted**: Decision is currently in effect
* **Proposed**: Under evaluation
* **Deferred**: Explicitly postponed

---

## ADR-001: Adopt a Session-Based Learning Model

**Status**: Accepted
**Date**: 2026-01-22

### Context

The platform must represent real learning behavior without relying on automatic activity tracking.
Learning often happens during live Zoom classes or intentional self-study periods.

Automatic detection (screen tracking, app monitoring) is intentionally avoided in Phase 1.

### Decision

Introduce a **Learning Session** as a first-class domain concept.

* Sessions are started and stopped manually
* All learning activities belong to exactly one session
* Sessions represent *intentional learning*, not inferred behavior

### Consequences

**Positive**

* Matches real user behavior
* Simple and predictable
* Avoids OS-level complexity and privacy concerns

**Negative**

* Relies on user discipline
* Idle or distracted time is not captured

---

## ADR-002: Separate Learning Activities from Sessions

**Status**: Accepted
**Date**: 2026-01-22

### Context

Within a single study session, the user performs multiple types of learning
(listening, reading, grammar, kanji, exercises), each with different materials and outputs.

Embedding all logic directly into sessions would reduce flexibility.

### Decision

Model **Learning Activity** as a separate domain entity.

* Activities belong to a session
* Activity types are extensible
* Activities may reference multiple materials

### Consequences

**Positive**

* Clear domain separation
* Supports future analytics and workflows
* Enables incremental feature growth

**Negative**

* Adds domain complexity early
* Requires coordination between services

---

## ADR-003: Treat Learning Materials as Reusable Assets

**Status**: Accepted
**Date**: 2026-01-22

### Context

Learning materials (audio, PDFs, text, images) are reused across sessions and activities.
They should not be tightly coupled to a single learning action.

### Decision

Introduce **Material** as an independent domain.

* Materials are uploaded once
* Referenced by activities
* Stored separately from metadata

### Consequences

**Positive**

* Prevents duplication
* Enables long-term content reuse
* Supports future sharing scenarios

**Negative**

* Adds indirection to simple flows
* Requires access control later

---

## ADR-004: Model User Outputs as First-Class Artifacts

**Status**: Accepted (Refined by ADR-009)
**Date**: 2026-01-22

### Context

User-generated outputs (notes, annotated images, answers) are central to learning value.
Treating them as simple attachments limits future evolution.

### Decision

Introduce **Artifact** as a first-class domain entity.

* Artifacts belong to activities
* Represent learning outputs
* Editable and versionable in future phases

### Consequences

**Positive**

* Enables real learning workflows
* Supports creative reuse
* Aligns with author-focused platform vision

**Negative**

* Increased storage complexity
* Requires richer UI support

---

## ADR-005: Use Logical Microservices with Deferred Physical Separation

**Status**: Accepted
**Date**: 2026-01-22

### Context

The project aims to practice microservice architecture, but early physical distribution
introduces unnecessary operational complexity.

### Decision

Design services with **clear bounded contexts**, while allowing:

* Single deployment initially
* Independent data ownership per service
* Future extraction without major refactoring
* This decision is exploratory and may be revised once real operational pain is observed.

### Consequences

**Positive**

* Encourages correct service boundaries
* Reduces early operational overhead
* Facilitates architectural learning

**Negative**

* Not a fully distributed system initially
* Requires discipline to avoid boundary erosion

---

## ADR-006: Incremental Adoption of Event-Driven Architecture

**Status**: Deferred
**Date**: 2026-01-22

### Context

Future requirements include analytics, orchestration, and Saga-based workflows.
Current Phase 1 flows are simple and synchronous.

### Decision

* Use REST for primary interactions in Phase 1
* Publish domain events for major state changes
* Defer full asynchronous orchestration

### Consequences

**Positive**

* Smooth learning curve
* Enables future Saga implementation
* Avoids premature complexity

**Negative**

* Some synchronous coupling remains
* Mixed communication styles later

### NOTE

* This ADR exists to record an idea, not a commitment.

---

## ADR-007: Defer Automatic Activity Tracking

**Status**: Deferred
**Date**: 2026-01-22

### Context

Ideas such as OS-level telemetry, screen tracking, or automatic Zoom detection were considered.

### Decision

Explicitly defer all automatic tracking mechanisms.

* No background PC monitoring
* No browser extensions
* No OS hooks

### Consequences

**Positive**

* Simpler system
* Clear ethical boundaries
* Faster delivery

**Negative**

* Relies entirely on manual input
* Less passive data collection

---

## ADR-008: Defer Creative Identity / Character System

**Status**: Deferred
**Date**: 2026-01-22

### Context

The idea of shared characters or avatars across games is conceptually appealing
but outside current technical scope.

### Decision

* Acknowledge the concept
* Do not model or implement it in Phase 1
* Revisit after platform stabilizes

### Consequences

**Positive**

* Prevents scope explosion
* Keeps focus on usable core platform

**Negative**

* No immediate progress on identity portability
* Requires future redesign

---

## ADR-009: Refine Artifact Ownership to Activity-Material Context

**Status**: Accepted
**Date**: 2026-02-13

### Context

ADR-004 established Artifact as a first-class entity belonging to an Activity.

However, a single Activity may reference multiple Materials, and user outputs are
usually created in response to a specific Material within that Activity.

If Artifacts belong only to Activity, the system cannot clearly associate
which outputs correspond to which material without additional metadata hacks
or fragile filtering logic.

This creates ambiguity when:

* An activity contains multiple audio files or documents
* The user reopens an activity and expects outputs grouped by material
* Analytics need to evaluate performance per material

### Decision

Refine the ownership model:

* Artifact belongs to an Activity’s usage of a Material
* Introduce an implicit or explicit **ActivityMaterial** association entity
* Artifact references this ActivityMaterial context

This preserves:

* Activity as learning intent
* Material as reusable resource
* Artifact as contextualized learning output

Artifacts do NOT belong directly to global Material,
as Materials are reusable across sessions and activities.

### Consequences

**Positive**

* Clear ownership boundaries
* Accurate reopening experience (outputs grouped per material)
* Cleaner analytics and reporting
* Prevents future redesign caused by ambiguous relationships

**Negative**

* Additional domain complexity
* Requires more explicit UI grouping

---

## Notes

* New ADRs should be appended to this document
* Existing ADRs may be revised, but changes should be documented
* Volatile decisions should prefer ADRs over README updates
