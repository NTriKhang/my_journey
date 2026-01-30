# Learning Session Service

## Purpose
Manages the lifecycle of a learning session (start / stop).

## Architecture
Clean Architecture:
Infrastructure → Application → Domain
API (Host) ───────┘

## Domain Model
- LearningSession (entity)
- SessionStatus (value object)

## Use Cases
- StartSession
- StopSession
- GetActiveSession

## Boundaries
- Does NOT manage activities
- Does NOT store materials
- Does NOT perform analytics

## Persistence
- Owns its database schema
- No cross-service data access

## LearningSession (Domain)

Purpose:
Represents an intentional period of learning.

Rules:
- Must have StartedAt
- Starts in Active state
- Can be Ended only once
- Contains zero or more LearningActivities

Lifecycle:
Active → Completed

Notes:
- No automatic tracking
- Time accuracy is user-provided
