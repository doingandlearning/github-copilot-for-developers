# Core Prompt Engineering Principles

**Module 2 — GitHub Copilot for Developers**

<!-- end_slide -->

## Opening scenario

A developer on your team asks AI:

> "Write some code for ASP.NET Core"

The output is generic boilerplate — a Hello World controller, nothing close to what they needed.

**Type in chat: what's the single most important thing missing from that prompt?**


<!-- end_slide -->

## The three Cs

Every developer prompt that works has three things.

**Clarity** — what do you want the model to do?
Task: generate, explain, refactor, test, debug

**Context** — what does the model need to know?
Stack, version, existing code, file, constraints

**Constraints** — what are the rules?
Style, patterns, things to avoid

<!-- pause -->

That prompt had none of them. Let's look at each one.

<!-- end_slide -->

## Clarity: task and format

Vague:
> "Write a controller"

<!-- pause -->

Clear:
> "Generate an ASP.NET Core REST controller with one GET endpoint that returns a list of users as `ActionResult<IEnumerable<UserDto>>`"

<!-- pause -->

The task is the verb. The format is what the output should look like.
If you can't state both in one sentence, the prompt isn't ready.

<!-- end_slide -->

## Context: stack, file, state

No context:
> "Generate a controller"

<!-- pause -->

With context:
> "I'm working in an ASP.NET Core 8 project using C# 12 and constructor injection throughout. Add a new controller for the Users resource. The `UserService` is already registered in DI."

<!-- pause -->

Context tells the model what world it's operating in. Without it, it guesses — and it guesses the average of everything it's ever seen, which is rarely what you need.

<!-- end_slide -->

## Constraints: style, patterns, don'ts

No constraints:
> "Generate an ASP.NET Core controller"

<!-- pause -->

With constraints:
> "Use constructor injection — not service-locator style. Return `ActionResult<UserDto>` with explicit null handling. No `dynamic` return types. Follow the pattern in the existing `ProductsController`."

<!-- pause -->

Constraints are how you encode your team's standards. The model doesn't know your codebase — you have to tell it what good looks like.

<!-- end_slide -->

## Discussion — type in chat

Look at these two prompts. Both are asking for the same thing.

**Prompt A:**
> "Explain what `[ApiController]` does"

**Prompt B:**
> "I'm new to ASP.NET Core 8. Explain what `[ApiController]` does — specifically what it changes about model binding and error responses compared to not having it. Give a concrete example of a difference."

**Which produces more useful output for a developer joining a new team — and why?**


<!-- end_slide -->

## Zero-shot, few-shot, chain-of-thought

These aren't techniques to memorise — they're descriptions of what you're already doing.

**Zero-shot**: just ask. Works for well-known patterns and quick lookups.
> "Explain what `IActionResult` is"

<!-- pause -->

**Few-shot**: give an example, ask for another. Works when style or format matters.
> "We write services like this: [example]. Generate a similar one for `OrderService`."

<!-- pause -->

**Chain-of-thought**: ask for reasoning before the answer. Works for complex tasks where accuracy matters.
> "First explain what this method does, then identify any issues, then show a refactored version."

<!-- end_slide -->

## When to use which

| Technique | Use when |
|---|---|
| Zero-shot | Well-known patterns, quick answers |
| Few-shot | You want specific style or format |
| Chain-of-thought | Complex refactoring, debugging, design decisions |

<!-- pause -->

**The practical rule:** start zero-shot. If the output doesn't match your needs, add an example (few-shot) or ask for reasoning first (chain-of-thought). Don't reach for complexity before you need it.

<!-- end_slide -->

## Iterative refinement

The first output is rarely the right output. That's not a failure — it's the process.

**Read the output as a diagnosis:**
- Wrong version or API? → Missing context
- Wrong style or patterns? → Missing constraints or a few-shot example
- Plausible but subtly incorrect? → Add chain-of-thought; ask it to reason before answering

<!-- pause -->

**Demo:** *(Run a vague prompt live — "Refactor this code" with no context — show the output, then diagnose it together. Ask the group: what would you add?)*

<!-- end_slide -->

## Refinement in practice

Starting prompt:
> "Refactor this code"

<!-- pause -->

After one round:
> "Refactor this C# method to use LINQ instead of a foreach loop. Keep the return type as `List<string>`."

<!-- pause -->

After another:
> "Refactor this C# method to use LINQ instead of a foreach loop. Keep the return type as `List<string>`. Filter out null entries rather than throwing. Here's the method: [code]"

<!-- pause -->

Each round adds one thing. You don't need to write the perfect prompt first time — you need to read the output and know what to add.

<!-- end_slide -->

## Summary

1. **Clarity**: state the task and the output format
2. **Context**: give the model the world it's operating in
3. **Constraints**: encode your standards — style, patterns, don'ts
4. **Techniques**: zero-shot first, add examples or reasoning when needed
5. **Iteration**: read the output as a diagnosis, not a verdict

<!-- end_slide -->

## Bridge to Module 3

**We've established:**
- How to write prompts that work (3Cs)
- How to recover when they don't (iteration)

**Module 3**: AI-assisted .NET/C# development — applying these techniques to boilerplate generation, refactoring, and test writing.

The 3Cs apply to every prompt in Module 3. If the output isn't right, you now know how to fix it.

<!-- end_slide -->

# Questions?

*Module 2 — Core Prompt Engineering Principles*