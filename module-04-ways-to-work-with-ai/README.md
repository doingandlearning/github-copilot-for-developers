# Module 4 — Three Ways to Work with AI

## Overview

This module replaces the original "Tooling Strategies (Mixed Environment)" content. It is written specifically for an audience using **Visual Studio with GitHub Copilot for Business**. The sidecar workflow and mixed-IDE content has been removed.

The module covers three distinct ways of working with AI in a development context:

1. **Inline completion** — using Copilot's suggestions as you type, with deliberate signal control
2. **Copilot Chat** — using the chat surface for exploration, explanation, and larger generation tasks
3. **PRD-driven development** — writing a structured spec first, then generating a multi-file feature in layers

## Audience

- Visual Studio (2022) with GitHub Copilot for Business
- Primarily .NET / C# (ASP.NET Core 8, EF Core)
- Mixed experience with Copilot: some daily users, some occasional — all IDE-integrated

## Learning Objectives

By the end of this module, delegates will be able to:

- Apply inline completion effectively, using signal techniques to improve suggestion quality
- Use Copilot Chat slash commands (`/explain`, `/fix`, `/tests`) on real code
- Reference specific files in chat using `#file:` syntax
- Write a PRD-style spec in both markdown and user story format
- Use a spec to generate a consistent multi-layer feature in sequence

## Module Structure

| File | Description |
|---|---|
| `slides.md` | Reveal.js-compatible slide deck |
| `TEACHING_NOTES.md` | Facilitator notes with demo sequences and attendee-specific guidance |
| `exercises/README.md` | Four delegate exercises covering all three modes |

## Time

Approximately 100 minutes including exercises:

- Lecture and demos: ~80 minutes
- Exercises: ~20 minutes (select two or three depending on time)

## Key Changes from Original Module 4

- Removed: sidecar workflow, mixed-IDE content, NetBeans/IntelliJ framing
- Removed: "mixed environments" section (not relevant to this cohort)
- Added: inline signal techniques (method naming, comment-first, pattern-start)
- Added: Copilot Chat slash commands with live demo sequences
- Added: `#file:` referencing
- Added: PRD-driven development — single feature and full vertical slice
- Added: both markdown spec and user story spec formats
- Attendee-specific notes in TEACHING_NOTES.md for Chris (testing), Tajinder (hallucination/safety), Alper (team workflow), Steven (ecosystem)
