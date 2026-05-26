# Module 3 — Exercises

**For delegates.** These exercises run during the Module 3 session. Your facilitator will manage timing and breakout rooms.

---

## Objective

By the end of these exercises you will be able to:

- Generate ASP.NET Core 8 code incrementally and evaluate each layer before proceeding
- Use layered explanation prompts to build understanding of unfamiliar code
- Refactor safely — with tests before and after — and verify the behaviour is unchanged
- Generate xUnit/Moq tests, run them, and diagnose failures as prompt refinement opportunities

---

## Scenario

You're working on a Library Management System — an ASP.NET Core 8 application managing books, authors, and loans. A new developer has been using AI to accelerate their work, and you're reviewing their output before it goes into the codebase.

**Remember:** Use only synthetic examples or generic ASP.NET Core patterns — not proprietary code or internal APIs.

---

## Exercise 1: Generate incrementally — and catch the mistake

**Individual (10 minutes), then breakout (5 minutes)**

Your task is to generate a `Loan` entity layer using AI. The catch: you're going to do it in stages, and at least one stage will produce output you shouldn't accept.

**Stage 1:** Generate the entity.

The `Loan` entity needs:
- `id`: long, primary key, auto-generated
- `bookId` and `userId`: long, foreign key references
- `loanDate`: DateOnly
- `returnDate`: DateOnly? (nullable)
- `status`: string — values are `ACTIVE`, `RETURNED`, `OVERDUE`

Write and run your prompt. Before moving to stage 2, apply the evaluation checklist: version, conventions, annotations, security. **Write down one specific thing you had to fix or refine.**

**Stage 2:** Add the repository abstraction. Feed the entity output into your next prompt as context.

**Stage 3:** Add a `LoanService` with a `FindActiveLoans()` method that returns loans with status `ACTIVE`.

**In your breakout room:** Share what you caught in stage 1. Did you both find the same issue, or different ones? **Agree on which checklist item — version, conventions, annotations, security — is most likely to catch a real problem in your team's actual codebase, and why.**

---

## Exercise 2: Explain legacy code in layers

**Individual (10 minutes), then whole group**

Here's a legacy method you've just inherited. You need to understand it well enough to know whether it's safe to refactor.

```csharp
public class OrderProcessor
{
    private readonly List<Order> orders = new();
    private readonly Dictionary<string, decimal> totals = new();

    public void ProcessOrders()
    {
        foreach (var order in orders)
        {
            if (order?.Status == "PENDING" && !string.IsNullOrWhiteSpace(order.CustomerId))
            {
                if (!totals.TryGetValue(order.CustomerId, out var currentTotal))
                {
                    currentTotal = 0m;
                }
                totals[order.CustomerId] = currentTotal + order.Amount;
            }
        }
    }

    public decimal GetTotalForCustomer(string customerId)
    {
        return totals.TryGetValue(customerId, out var total) ? total : 0m;
    }
}
```

Run four prompts, in order:

1. What does this code do? (functional behaviour, input/output)
2. Why might it have been written this way? (historical context, patterns of the era)
3. What would the modern C# 12 equivalent look like?
4. What would break, or need verifying, if you changed it?

**For prompt 4 specifically:** the model doesn't know your codebase. What does it tell you to check — and what questions does it raise that only you can answer by reading the actual code?

**Type in chat:** one thing the model told you about this code that you wouldn't have noticed by reading it yourself.

---

## Exercise 3: Refactor safely

**Breakout — 15 minutes**

Using the `OrderProcessor` from Exercise 2, your task is to refactor `ProcessOrders()` to modern C# 12 style — LINQ, nullable reference types, improved readability. But you're going to do it the safe way.

**Step 1:** Ask AI to generate xUnit tests for the existing `ProcessOrders()` method. Ask specifically for: a happy path with two pending orders, a test where one order has a null `CustomerId`, and a test where the order list is empty.

**Step 2:** Run the tests. If any fail, diagnose why — is the test wrong, or has the model misunderstood the method's behaviour? Fix the tests until they pass against the original code.

**Step 3:** Ask AI to refactor `ProcessOrders()` to use LINQ and C# 12 features. Specify that behaviour must be identical.

**Step 4:** Run the tests against the refactored version.

**In your breakout room — the real question:** Does the refactored version handle the case where `order` itself is null the same way the original does? Look at the original carefully: `order?.Status` uses null-conditional access, so a null order is silently skipped. Does the refactored version do the same? **If your pair disagrees about whether the behaviour is equivalent, that's the most important thing to feed back to the room.**

---

## Exercise 4: Generate and interrogate tests

**Individual (10 minutes), then breakout (5 minutes)**

Here's a service method. Your job is to generate tests for it — and then find the scenario the model didn't think of.

```csharp
public class BookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public Book? FindBookByIsbn(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
        {
            return null;
        }
        return _repository.FindByIsbn(isbn);
    }
}
```

**Step 1:** Write a prompt asking for xUnit and Moq tests covering: valid ISBN returns the book, null input returns null without calling the repository, empty string returns null without calling the repository, and ISBN not found returns null.

**Step 2:** Run the tests. Note any that fail and what the failure message is.

**Step 3:** Now find the gap. The guard clause uses `IsNullOrWhiteSpace`. Does the model test for a whitespace-only ISBN — `"   "` — as a distinct case from empty string? If not, add it. Does the behaviour match what you'd expect?

**In your breakout room:** Did you find any other scenarios the model missed? **Agree on a rule: what categories of test scenario does AI reliably generate, and what categories does it consistently miss?**

---

## Extensions

If you finish early or want to go deeper:

1. **Generate a REST controller:** Add a `LoansController` with `GET /loans/active` that calls `FindActiveLoans()`. Review it with the full evaluation checklist. What did you have to refine?

2. **Constraint stress-test:** Take a prompt that generated good output and deliberately remove one constraint — the version, the injection style, or a specific annotation. How much does the output degrade?

3. **Layer 4 in depth:** Take any method from your own codebase (anonymised if needed) and run only layer 4 — "what would break if I changed this?" Compare what the model raises against what you know. What did it miss?

4. **Integration test prompts:** Ask AI to generate an integration test for `LoanService` using `WebApplicationFactory`. What's different about the prompt structure compared to a unit test prompt?

---

## Before you move to Module 4

Make sure you have:

- [ ] Generated a complete entity layer incrementally — and caught at least one thing to fix (Exercise 1)
- [ ] Run all four explanation layers on the legacy code — and identified what layer 4 can't answer without your own codebase knowledge (Exercise 2)
- [ ] Refactored with tests before and after — and verified the null-order behaviour specifically (Exercise 3)
- [ ] Found at least one test scenario the model missed and added it (Exercise 4)

In Module 4 we'll look at three ways to work with AI — inline completion, Copilot Chat, and PRD-driven development. The evaluation checklist applies to output from all three.