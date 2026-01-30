# Japanese Learning Platform

## Overview

This project is a **personal learning platform** designed to support structured Japanese study while serving as a **practical environment for practicing microservice architecture**.

The platform captures intentional learning activities (classes, exercises, materials, outputs) and models them using clear domain boundaries suitable for service-oriented and event-driven architectures.

Phase 1 targets **single-user, real daily usage**. Multi-user and social capabilities are intentionally deferred.

---

## Objectives

- Support real Japanese learning workflows:
  - Live classes (e.g. Zoom)
  - Listening, reading, grammar, kanji practice
  - Exercises with editable outputs
- Centralize learning materials and results
- Provide a realistic domain to practice:
  - Service boundaries
  - Event-driven design
  - Saga and eventual consistency (later phases)

---

## Non-Goals (Phase 1)

- Automatic activity tracking (OS, browser, or screen-level)
- Browser extensions
- Flashcard-focused learning
- Game engines or gameplay logic
- AI-generated content

---

## Core Domain Concepts

### Learning Session
Represents an **intentional study period**, explicitly started and stopped by the user.

Examples:
- Live online class
- Self-study block

---

### Learning Activity
Represents a specific type of learning within a session.

Examples:
- Listening
- Reading
- Grammar
- Kanji
- Exercise

---

### Learning Material
Reusable learning resources.

Examples:
- Audio files
- PDFs
- Text
- Images

---

### Artifact
User-generated learning outputs.

Examples:
- Notes
- Annotated exercise images
- Written answers

Artifacts are treated as first-class domain entities.

---

## High-Level Architecture

### Logical Services

| Service | Responsibility |
|------|---------------|
| Session Service | Manage learning sessions |
| Activity Service | Manage learning activities |
| Material Service | Store and retrieve materials |
| Artifact Service | Manage learning outputs |

> In early phases, services may be deployed together while maintaining strict logical boundaries.

---

## Technology Stack (Initial)

### Backend
- C# (.NET) or Go
- REST APIs (Phase 1)
- Event publication (incremental)

### Frontend
- Next.js (TypeScript)

### Storage
- Relational database for metadata
- Object storage for files (audio, PDF, images)

---

## Typical User Flow

1. User starts a learning session manually
2. User creates activities within the session
3. Materials are uploaded or linked
4. Exercises produce artifacts (notes, images)
5. User stops the session

This flow prioritizes **intentional learning** over inferred behavior.

---

## Architecture Practice Scope

This project is intentionally designed to support:

- Bounded contexts
- Independent data ownership
- Event-driven evolution
- Saga pattern experimentation (future phase)

Detailed architectural decisions are documented separately in **Architecture Decision Records (ADR)**.

---

## Documentation

- `README.md` — Project overview and scope
- `docs/architecture/ADR.md` — Architecture Decision Records

---

## Project Status

**Phase 1 – Active Development**

This document is expected to evolve as:
- Real usage reveals new requirements
- Architectural decisions are validated or revised
- Scope expands incrementally

---

## Notes

- This is not a production SaaS
- Stability of architecture is prioritized over feature completeness
- Over-engineering is intentionally avoided in early phases
