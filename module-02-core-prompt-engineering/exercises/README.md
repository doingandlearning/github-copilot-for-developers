# Module 2 — Exercises

**For delegates.** These exercises run during the Module 2 session. Your facilitator will manage timing and breakout rooms.

---

## Objective

By the end of these exercises you will be able to:

- Diagnose what's wrong with a prompt that produces poor output
- Apply the three Cs to produce output that's actually usable
- Choose the right prompting technique for the task
- Give and receive specific, actionable feedback on a prompt

---

## Scenario

You're a C# developer on an ASP.NET Core 8 project. A new team member has been using AI to help with coding tasks but keeps getting output that doesn't match the team's codebase or style. Your job in these exercises is to fix their prompts — and then have yours fixed in return.

**Remember:** Use only synthetic examples or generic ASP.NET Core patterns — not proprietary code or internal APIs.

---

## Exercise 1: Diagnose and rewrite

**Individual (5 minutes), then breakout (5 minutes)**

Here are three prompts a junior developer wrote. Each one produced unhelpful output. For each prompt, identify the specific problem and rewrite it.

**Prompt A:**
> "Write some code"

**Prompt B:**
> "Explain dependency injection"

**Prompt C:**
> "Refactor this so it's better"

For each one, write:
- What's missing (clarity, context, constraints — or more than one)
- A rewritten version that would produce genuinely useful output for an ASP.NET Core 8 project

**In your breakout room:** Compare rewrites. Where you made different choices — different context, different constraints — decide which version is more useful and why. **One person feeds back: which rewrite did you converge on, and what rule did you use to pick it?**

---

## Exercise 2: Few-shot in practice

**Individual (8 minutes), then whole group**

Your team has a consistent service class style. You want AI to generate a new one that matches it exactly — not the generic pattern it would produce on its own.

**Your task:**

Write a few-shot prompt that:
- Shows the model one example of your team's style (use the synthetic example below)
- Asks it to generate a `OrderService` following the same pattern
- Includes at least one explicit constraint about what the output must or must not do

**Use this as your example service:**

```csharp
public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository _repository)
    {
        this._repository = repository;
    }

    public IEnumerable<Product> GetAll() =>
        _repository.GetAll();

    public Product? FindById(long id) =>
        _repository.FindById(id);
}
```

**Then:** Run your prompt in GitHub Copilot Chat or an approved tool. Does the output match the example's style — interface-typed repository, expression-bodied methods, nullable return on `Find`? What did it get right, and what did you have to add in a second prompt?

**Type in chat:** one thing the model got right, and one thing it missed or got wrong.

---

## Exercise 3: Chain-of-thought for a real decision

**Breakout — 10 minutes**

This exercise is about a prompt that has to be right — not just plausible.

**Scenario:**
Your team is debating whether to use `IActionResult` or `ActionResult<T>` as the return type for controller endpoints in your new ASP.NET Core 8 API.

**Your task:**

Write a chain-of-thought prompt that asks the model to reason through this decision before giving a recommendation. The prompt should:
- State the context (ASP.NET Core 8, new API, OpenAPI documentation is important)
- Ask for reasoning before a recommendation — not just "which is better"
- Include at least one constraint about what a good answer looks like (e.g. it should address Swagger/OpenAPI generation, it should consider testability)

**In your breakout room:** Write the prompt together, then run it. Read the output — does the reasoning actually address the constraints you set, or does it give a generic answer that ignores them? If it ignored a constraint, why? What would you add?

**Feed back to the room:** the constraint the model ignored, and what you'd do differently.

---

## Exercise 4: Swap and critique

**Breakout — 10 minutes**

**Your task:**

1. Write a prompt for one of these tasks (your choice):
   - Ask AI to explain what `[FromBody]` does and when to use it versus `[FromQuery]`
   - Ask AI to generate an Entity Framework Core repository with a `FindByEmail` method
   - Ask AI to suggest a refactor for a method with more than three responsibilities

2. Swap prompts with someone in your breakout room. Run their prompt. Then answer:
   - Does the output actually solve the task they described?
   - What is the single most important thing missing — clarity, context, or constraints?
   - Write one specific rewrite of the weakest part of their prompt

3. Share the rewrite with them and discuss whether they agree it improves it.

**Back in the main room:** Did anyone disagree with the feedback they got? What was the disagreement — about what the prompt needed, or about what good output looks like?

---

## Extensions

If you finish early or want to go deeper:

1. **Failure modes:** Ask AI the same question three times with slightly different phrasing. How much does the output vary? What does that tell you about how fragile prompt design is?

2. **Constraint stress-test:** Take a prompt that worked well and deliberately remove one constraint. Does the output degrade? Which constraint mattered most?

3. **Template for your team:** Write a prompt template your team could reuse for a common task — refactoring, test generation, or controller scaffolding. Focus on the context and constraints that are always true for your codebase, with placeholders for what changes each time.

---

## Before you move to Module 3

Make sure you have:

- [ ] Diagnosed at least three weak prompts and rewritten them (Exercise 1)
- [ ] Written and run a few-shot prompt — and noted what it got wrong (Exercise 2)
- [ ] Used chain-of-thought for a decision that needed reasoning, not just an answer (Exercise 3)
- [ ] Given and received specific prompt feedback (Exercise 4)

In Module 3 you'll apply these techniques directly to .NET/C# development tasks — boilerplate, refactoring, and test generation. The 3Cs apply to every prompt. If the output isn't right, you now know how to diagnose it.