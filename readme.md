# Prompt Engineering for Developers

**A one-day, in-person course for software development teams using GitHub Copilot for Business.**

---

## What this course is

This course is for developers who are already using AI tools - or about to - and want to get consistent, production-quality results from them. It is not an introduction to what AI is. It is a practical course in how to work with it well.

The focus is GitHub Copilot for Business used within Visual Studio or VSCode, with prompt engineering principles that apply across any AI surface. By the end of the day, delegates will have a working mental model for when to use which AI mode, how to write prompts that produce usable output first time, and how to build a feature from a structured spec rather than a sequence of disconnected prompts.

---

## Who it is for

Developers working in .NET/C# (ASP.NET Core, Entity Framework Core) who have Copilot available and want to use it more deliberately. No prior prompt engineering knowledge is assumed. Some familiarity with Copilot - even just a few weeks - is helpful.

---

## Learning outcomes

By the end of the day, delegates will be able to:

- Explain how large language models work, where they fail, and how to verify their output
- Write prompts that are clear, contextual, and constrained — and diagnose why a prompt is producing poor output
- Use AI for code generation, explanation, refactoring, and test writing in a .NET context
- Choose between inline completion, Copilot Chat, and PRD-driven development based on the task
- Write a structured feature spec that produces consistent, multi-layer output from Copilot Chat

---

## Course structure

### Module 1 — Introduction to GenAI for Developers

**~60 minutes**

How LLMs work, where they fail, and how to use them safely. Covers the distinction between public AI tools and enterprise instances, the hallucination problem and how to catch it, and a practical framework for deciding when to trust AI output and when not to.

### Module 2 — Core Prompt Engineering Principles

**~75 minutes including lab**

The fundamentals of writing prompts that work. Clarity, context, and constraints as a framework. Zero-shot vs. few-shot prompting. Chain-of-thought for accuracy. Iterative refinement — how to read bad output and adjust the prompt rather than starting again.

### Module 3 — AI-Assisted .NET/C# Development

**~90–120 minutes**

The anchor module. Generating boilerplate, entities, service layers, and DTOs. Using AI to explain and modernise legacy code. Refactoring strategies. Generating xUnit/Moq test suites with meaningful coverage. How to review AI output before applying it.

### Module 4 — Three Ways to Work with AI

**~100 minutes including exercises**

A practical framework for the three modes of working with Copilot:

- **Inline completion** — using signal to steer suggestions; accepting word-by-word rather than wholesale
- **Copilot Chat** — slash commands (`/explain`, `/fix`, `/tests`), file referencing, and using chat for design decisions
- **PRD-driven development** — writing a structured spec (markdown or user story format) and generating a consistent multi-file feature in layers

---

## Each module contains

- `slides.md` — Markdown Reveal.js-compatible slide deck
- `exercises/README.md` — delegate-facing exercise instructions

---

## Notes on scope

This course covers GitHub Copilot as an AI assistant - inline completion and Copilot Chat - not GitHub as a source control or DevOps platform. Pull requests, GitHub Actions, GitHub Issues, and related platform features are out of scope.

---

## Prerequisites for delegates

- Visual Studio 2022 or VSCode with the GitHub Copilot extension installed
- GitHub Copilot for Business licence active
- A .NET/C# project available to work with during exercises, or use the provided teaching examples
