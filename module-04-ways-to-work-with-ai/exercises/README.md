# Module 4 — Exercises

**For delegates.** Work through these individually or in pairs. Exercises are designed to be done in Visual Studio with GitHub Copilot for Business active.

---

## Objective

By completing these exercises, you will:

- Apply all three modes (inline, chat, PRD-driven) to concrete tasks
- Practise providing better signal to inline completion
- Use Copilot Chat slash commands on code you recognise
- Write a spec and use it to generate a multi-layer feature

---

## Exercise 1: Inline — signal and signal quality

**Your task:**

Open a new C# file. You're going to generate a simple entity class three times, with increasing signal each time.

**Round 1 — minimal signal:**

Type only this and trigger inline completion:

```csharp
public class Task
{
```

Note what Copilot suggests. Accept nothing yet — just observe.

**Round 2 — better signal:**

Replace the class with this and trigger again:

```csharp
// Represents a task in a project management system.
// Properties: Id, Title (required, max 100 chars), Description (optional),
// Priority (enum: Low, Medium, High), CreatedAt (UTC), OwnerId (int).
public class TaskItem
{
```

Note the difference in the suggestion.

**Round 3 — pattern signal:**

Write the first two properties manually, then stop and let Copilot continue:

```csharp
public class TaskItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    // stop here and trigger
```

**Reflect:**

- Which round produced the most usable output?
- What does this tell you about when to reach for inline vs. chat?

**Time:** About 10 minutes.

---

## Exercise 2: Chat — `/explain`, `/fix`, `/tests`

**Your task:**

Use the following service class. Paste it into a new file in your project.

```csharp
public class TaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskItem> CreateAsync(CreateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Title is required");

        if (request.Title.Length > 100)
            throw new ArgumentException("Title cannot exceed 100 characters");

        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            CreatedAt = DateTime.UtcNow,
            OwnerId = request.OwnerId
        };

        return await _repository.SaveAsync(task);
    }

    public async Task<IEnumerable<TaskItem>> GetByOwnerAsync(int ownerId, Priority? filter = null)
    {
        var tasks = await _repository.GetByOwnerAsync(ownerId);
        if (filter.HasValue)
            tasks = tasks.Where(t => t.Priority == filter.Value);
        return tasks.OrderByDescending(t => t.CreatedAt);
    }
}
```

**Part A — `/explain`:**

Select the entire `GetByOwnerAsync` method. Run `/explain` in Copilot Chat. Read the output.

- Does it match what you expected?
- Is there anything in the explanation that surprises you or that you'd want to verify?

**Part B — `/fix`:**

Add this deliberately broken method to the class:

```csharp
public async Task DeleteAsync(int taskId, int requestingUserId)
{
    var task = await _repository.GetByIdAsync(taskId);
    task.DeletedAt = DateTime.Now;
    await _repository.SaveAsync(task);
}
```

Select it and run `/fix`. Note what Copilot identifies. Does it catch:

- The missing null check on `task`?
- The use of `DateTime.Now` instead of `DateTime.UtcNow`?
- The missing ownership check?

**Part C — `/tests`:**

Select the entire `TaskService` class. Run `/tests`.

Review what's generated:

- Does it cover the validation paths (empty title, title too long)?
- Does it cover the filter logic in `GetByOwnerAsync`?
- Are the test names meaningful?

Follow up in chat: "Add edge case tests for null request, empty OwnerId, and filtering by each Priority value."

**Reflect:**

- How does `/tests` compare to writing tests from scratch?
- What would you still need to add manually?

**Time:** About 20 minutes.

---

## Exercise 3: PRD-driven — single feature

**Your task:**

Write a spec for a feature you know well from your own work — something straightforward, one endpoint or one service operation.

If you can't think of one, use this:

```
Feature: Update task priority

Endpoint: PATCH /api/tasks/{id}/priority

Request body:
- Priority (string, one of: Low, Medium, High)

Behaviour:
- Validate that the priority value is one of the allowed values
- Check that the task belongs to the requesting user (use OwnerId)
- Update the priority and save
- Return 200 with the updated task on success
- Return 404 if the task is not found
- Return 403 if the task belongs to a different user
- Return 400 if the priority value is invalid

Stack: ASP.NET Core 8, C# 12, EF Core
```

**Step 1:** Paste the spec into Copilot Chat and ask it to generate the request DTO with validation.

**Step 2:** Using the DTO output as context, ask it to generate the service method.

**Step 3:** Ask it to generate the controller endpoint.

**Step 4:** Ask it to generate unit tests that cover the acceptance criteria.

**Reflect:**

- Was the output consistent across layers (naming, error patterns)?
- Did the tests actually target the acceptance criteria you wrote?
- What would you have needed to refine or correct?

**Time:** About 20 minutes.

---

## Exercise 4: PRD-driven — write your own spec

**Your task:**

Think of a feature from your current or recent work that spans at least two layers (e.g. a controller that calls a service, or a service that calls a repository).

Write a spec for it in either format:

**Option A — Markdown spec:**

```markdown
## Feature: [name]

**Stack:** [your stack]

**Endpoint / Entry point:** [describe it]

**Request / Input:** [list the fields and types]

**Behaviour:** [list what it does, step by step]

**Acceptance criteria:**
- [ ] ...
- [ ] ...
```

**Option B — User story:**

```markdown
## User Story: [name]

**As a** [user type]
**I want to** [goal]
**So that** [reason]

**Acceptance criteria:**
- [ ] ...
- [ ] ...

**Technical notes:**
- Stack: ...
- Patterns to follow: ...
```

You do not need to generate the code in this exercise — the goal is to practise writing a spec that is clear enough to generate from.

**Then:** Swap specs with a partner. Read their spec. Ask yourself:

- Could you implement this from the spec alone?
- Is there anything ambiguous about the expected behaviour?
- Are the acceptance criteria testable?

Give them one piece of feedback.

**Time:** About 15 minutes.

---

## Final checklist

Before moving on, you should have:

- [ ] Observed how inline signal quality affects suggestion quality (Exercise 1)
- [ ] Used `/explain`, `/fix`, and `/tests` on real code (Exercise 2)
- [ ] Generated a multi-layer feature from a spec (Exercise 3)
- [ ] Written a spec that someone else could generate from (Exercise 4)

---

## If you finish early

**Extension 1:** Take the spec you wrote in Exercise 4 and generate the feature. Compare the output to what you would have written manually. What did you need to correct?

**Extension 2:** Take any spec from today and write it in the format you didn't use. If you used the markdown format, rewrite it as a user story. Notice what's easier to express in each format.

**Extension 3:** Add a "patterns to follow" section to your spec referencing a real file in your codebase. Use `#file:` in Copilot Chat to bring that file into scope. Does the output more closely match your existing codebase style?
