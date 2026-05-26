# Module 4 — Exercises

**For delegates.** These exercises run during the Module 4 session. Your facilitator will manage timing and breakout rooms. Exercises are designed to be done in VS Code or Visual Studio with GitHub Copilot for Business active.

---

## Objective

By the end of these exercises you will be able to:

- Read inline suggestions critically before accepting them
- Use `/explain`, `/fix`, and `/tests` and notice what each one misses
- Generate a multi-layer feature from a spec and identify where the output diverges from your intent
- Write a spec that someone else could generate from — and that you could use in a real code review

---

## Exercise 1: Inline — signal and what it changes

**Individual (10 minutes)**

Open a new C# file. You're going to generate a simple entity class three times, with increasing signal each time.

**Round 1 — minimal signal:**

Type only this and trigger inline completion:

```csharp
public class Task
{
```

Don't accept anything yet. Note what's suggested.

**Round 2 — named intent:**

Replace the class with this and trigger again:

```csharp
// Represents a task in a project management system.
// Properties: Id, Title (required, max 100 chars), Description (optional),
// Priority (enum: Low, Medium, High), CreatedAt (UTC), OwnerId (int).
public class TaskItem
{
```

**Round 3 — pattern started:**

```csharp
public class TaskItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
```

Stop there and let Copilot continue.

**The decision:** Based on what you observed, in which of these situations would you reach for inline rather than Chat?

- Writing a `CreateOrderRequest` DTO with eight fields you already know
- Writing a service method with a non-obvious null-handling requirement
- Completing a `switch` statement over a known enum you've already started
- Writing a repository interface for an entity you haven't defined yet

**Type your answers in chat (use inline / use Chat for each).** Be prepared to defend one you're unsure about.

---

## Exercise 2: Chat — what the slash commands catch and miss

**Individual (15 minutes), then breakout (5 minutes)**

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

Select `GetByOwnerAsync`. Run `/explain`. Read it.

Is there anything in the explanation that's subtly wrong, or that you'd want to verify? The method filters and sorts — does the explanation correctly describe *when* filtering happens relative to the database call?

**Part B — `/fix`:**

Add this method to the class:

```csharp
public async Task DeleteAsync(int taskId, int requestingUserId)
{
    var task = await _repository.GetByIdAsync(taskId);
    task.DeletedAt = DateTime.Now;
    await _repository.SaveAsync(task);
}
```

Select it and run `/fix`. This method has three problems:
- No null check on `task`
- `DateTime.Now` instead of `DateTime.UtcNow`
- No ownership check — any user can delete any task

**Does `/fix` catch all three?** Note which ones it flags and which it misses. If it misses one, follow up in Chat with a prompt that surfaces the missing issue.

**Part C — `/tests`:**

Select the entire `TaskService` class and run `/tests`. Then check:

- Does it cover the empty-title validation path?
- Does it cover the title-too-long path?
- Does it cover filtering by each `Priority` value in `GetByOwnerAsync`?

Follow up: `"Add a test for when GetByOwnerAsync is called with a null filter — what should it return?"`

**In your breakout room:** Compare what `/fix` caught and missed across your pair. **Agree on this: of the three problems in `DeleteAsync`, which is the most dangerous to miss — and why does it matter whether Copilot catches it or you have to?**

---

## Exercise 3: PRD-driven — generate and find the divergence

**Individual (20 minutes), then whole group**

Use the following spec. Your task is to generate the feature in layers — and find at least one place where the output diverges from the spec.

```markdown
## Feature: Update task priority

**Stack:** ASP.NET Core 8, C# 12, EF Core. Constructor injection throughout.
Follow the existing error response shape using ProblemDetails.

**Endpoint:** PATCH /api/tasks/{id}/priority

**Request:** Priority (string — must be one of: Low, Medium, High)

**Behaviour:**
- Validate that Priority is one of the allowed values
- Check the task belongs to the requesting user via OwnerId
- Update the priority and save
- Return 200 with the updated TaskDto on success
- Return 404 if the task is not found
- Return 403 if the task exists but belongs to a different user
- Return 400 with a field-level error if the priority value is invalid

**Acceptance criteria:**
- [ ] Invalid priority value returns 400 with a descriptive field error
- [ ] Task not found returns 404
- [ ] Task owned by another user returns 403 — not 404
- [ ] Successful update returns 200 with updated TaskDto
```

**Step 1:** Paste the spec and ask for the request DTO with validation.

**Step 2:** Using the DTO as context, ask for the service method.

**Step 3:** Ask for the controller endpoint.

**Step 4:** Ask for unit tests covering the acceptance criteria.

**The question to answer:** Where did the output diverge from the spec? Look specifically for:
- The 403 vs. 404 distinction — did the service implement it, or collapse it to a single case?
- The error response shape — does it use `ProblemDetails`, or a custom shape?
- Naming — is `Priority` spelled and cased consistently across all four layers?

**Type in chat:** the single most important divergence you found, and the one-line spec addition that would have prevented it.

---

## Exercise 4: Write a spec that could go into production

**Breakout — 15 minutes**

This is the exercise that produces something you can use next week.

Think of a feature from your current or recent work that spans at least two layers — a controller calling a service, or a service calling a repository. It doesn't have to be complex. A single endpoint that does one thing well is enough.

Write the spec using either format:

**Option A — markdown spec:**

```markdown
## Feature: [name]

**Stack:** [your stack and conventions]

**Endpoint / entry point:** [describe it]

**Request / input:** [fields, types, constraints]

**Behaviour:** [step by step, including error cases]

**Acceptance criteria:**
- [ ] ...
```

**Option B — user story:**

```markdown
## User Story: [name]

**As a** [user type]
**I want to** [goal]
**So that** [reason]

**Acceptance criteria:**
- [ ] ...

**Technical notes:**
- Stack: ...
- Patterns to follow: ...
```

**In your breakout room:** Swap specs. Read your partner's spec and answer three questions:

1. Could you implement this from the spec alone, without asking a clarifying question?
2. Is there any acceptance criterion that isn't testable as written?
3. Is the 403/404 distinction (or equivalent ownership/authorisation edge case) covered?

Give them one specific rewrite of the weakest acceptance criterion — not general feedback, a rewrite.

**Feed back to the room:** Did your partner find a gap you didn't notice when writing? What kind of gap was it — missing behaviour, untestable criterion, or ambiguous ownership rule?

---

## Extensions

If you finish early or want to go deeper:

1. **Generate your own spec:** Take the spec you wrote in Exercise 4 and generate the feature. Compare the output to what you'd have written manually. Note what you had to refine — and add those refinements back to the spec.

2. **`#file` in practice:** Add a `**Patterns to follow:**` section to your spec that references a real file — `#file:TasksController.cs`. Does the output more closely match your existing codebase conventions?

3. **Format comparison:** Take any spec from today and rewrite it in the format you didn't use. Which format makes acceptance criteria easier to express? Which makes the technical constraints clearer?

4. **Spec as documentation:** Take your finished spec and paste it into a Confluence page or README alongside the code it generated. Is it a useful description of the feature for someone who didn't write it?

---

## Before you finish

Make sure you have:

- [ ] Observed how signal quality affects inline suggestions — and know when to use Chat instead (Exercise 1)
- [ ] Used `/explain`, `/fix`, and `/tests` and noted what each one missed, not just what it found (Exercise 2)
- [ ] Generated a feature from a spec and identified at least one divergence — and the spec addition that would have prevented it (Exercise 3)
- [ ] Written a spec that someone else reviewed — and received one specific rewrite of a weak criterion (Exercise 4)

The spec you wrote in Exercise 4 is the most reusable thing you're leaving with. Keep it. Refine it against the first time you generate from it.