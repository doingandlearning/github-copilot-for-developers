# Demo 3: Refactoring with AI — modernizing safely

## Goal

Show delegates how to refactor verbose, procedural code to modern C# 12 style using LINQ, with emphasis on safety (tests before/after).

**Teaching style**: Problem-first approach, progressive building

---

## Before the demo

- Ensure you have access to an AI chat tool.
- Have an IDE ready with the sample code and tests.
- Prepare a test that passes before refactoring (critical for safety demo).
- Use only **synthetic** examples.

---

## Steps

**Problem-first approach**: Start with verbose code

### 1. Set up the problem (2 min)

**Say:** "Here's a method that works, but it's verbose and uses old C# patterns. How would you refactor it?"

**Show the verbose code** (example provided below).

**Point out the problems:**
- ❌ Nested loops
- ❌ Multiple null checks
- ❌ Verbose, hard to read
- ❌ Not using C# 12 features

**Say:** "Let's refactor it with AI, but safely — with tests."

---

### 2. Show the test first (2 min)

**Show the existing test** that passes.

**Say:** "Before refactoring, we have a test that passes. This is our safety net. After refactoring, this test should still pass."

**Run the test** — it passes.

**Talking point:** "Tests before refactoring = safety."

---

### 3. Generate the refactored code (4 min)

**Show the prompt:**

> "Refactor this method to use C# 12 LINQ. Replace the nested loops with LINQ, extract null checks into filters, and improve readability. Maintain the exact same behavior — the method should return the same results."

**Include the code:**

```csharp
public List<string> GetActiveUserEmails(List<User> users)
{
    var emails = new List<string>();
    foreach (var user in users)
    {
        if (user is not null && user.IsActive && !string.IsNullOrWhiteSpace(user.Email))
        {
            emails.Add(user.Email);
        }
    }
    return emails;
}
```

**Run the prompt** and show the refactored output.

**Point out:**
- ✅ Uses LINQ
- ✅ More readable
- ✅ Functional style
- ✅ Same logic (filters nulls, active users, non-empty emails)

---

### 4. Apply and test (3 min)

**Show the refactored code** in the IDE.

**Say:** "Now let's replace the old method with the refactored version and run the test."

**Run the test** — it should pass.

**If it passes:**
- ✅ "Tests pass — refactoring maintained behavior."

**If it fails:**
- Show how to diagnose: "What's different?"
- Refine prompt: "Fix the refactoring — the test expects..."
- Re-run: Tests pass

**Say:** "This is why we test — refactoring can introduce subtle bugs."

---

### 5. Compare before/after (2 min)

**Show side-by-side:**

**Before:**
- Nested loops
- Multiple null checks
- Verbose
- Hard to read

**After:**
- LINQ
- Functional style
- More readable
- Same behavior

**Say:** "Modern, readable code that does the same thing — that's successful refactoring."

---

### 6. Safety checklist (2 min)

**Show the refactoring safety checklist:**

- ✅ **Tests before:** Did we have tests?
- ✅ **Review diff:** Did we check what changed?
- ✅ **Tests after:** Do tests still pass?
- ✅ **Behavior:** Is functionality the same?

**Say:** "Never refactor without tests. If you don't have tests, write them first (with AI help!)."

---

## Sample code to refactor

**Verbose method** (synthetic):

```csharp
public List<string> GetActiveUserEmails(List<User> users)
{
    var emails = new List<string>();
    foreach (var user in users)
    {
        if (user is not null && user.IsActive && !string.IsNullOrWhiteSpace(user.Email))
        {
            emails.Add(user.Email);
        }
    }
    return emails;
}
```

**Refactored version** (what AI should generate):

```csharp
public List<string> GetActiveUserEmails(List<User> users)
{
    return users
        .Where(user => user is not null)
        .Where(user => user!.IsActive)
        .Select(user => user!.Email)
        .Where(email => !string.IsNullOrWhiteSpace(email))
        .ToList()!;
}
```

**Test** (should pass before and after):

```csharp
[Fact]
public void GetActiveUserEmails_ReturnsOnlyActiveUsersWithEmail()
{
    var users = new List<User?>
    {
        new() { Name = "John", Email = "john@example.com", IsActive = true },
        new() { Name = "Jane", Email = "jane@example.com", IsActive = false },
        new() { Name = "Bob", Email = null, IsActive = true },
        null
    };

    var emails = service.GetActiveUserEmails(users!);

    Assert.Single(emails);
    Assert.Equal("john@example.com", emails[0]);
}
```

---

## If you can't run live

- Show pre-prepared screenshots of before/after code.
- Use slides with the comparison.
- Emphasize the process: test → refactor → test → verify.

---

## Teaching Tips

- **Emphasize**: Safety first — always test before and after
- **Watch for**: Delegates who want to refactor without tests — show what breaks
- **Adapt**: If refactoring seems risky, emphasize test-first approach more

---

## Time Allocation

- Set up problem: 2 min
- Show test first: 2 min
- Generate refactored code: 4 min
- Apply and test: 3 min
- Compare before/after: 2 min
- Safety checklist: 2 min
- **Total: ~15 minutes**
