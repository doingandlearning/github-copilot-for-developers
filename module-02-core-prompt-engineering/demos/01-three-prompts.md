# Demo 1: Three prompts — vague → clear → constrained

## Goal

Show how adding clarity, context, and constraints improves output quality step by step.

**Teaching style**: Problem-first approach, progressive building

---

## Before the demo

- Open a chat interface (public or enterprise).
- Have the three prompts ready (see `prompts/vague-clear-constrained.txt`).
- Use synthetic ASP.NET Core examples only.

---

## Steps

**Problem-first approach**: Show bad output first, then improve

### 1. Set up the problem (1 min)

**Say:** "Let's see what happens when we start vague and progressively improve a prompt."

**Show the task:** Generate a REST controller for users.

---

### 2. Prompt 1: Vague (2 min)

**Show the prompt:**

> "Write some code for ASP.NET Core"

**Run it and show the output.**

**Point out problems:**

- ❌ Too vague — what kind of code?
- ❌ No context — which version? What's the task?
- ❌ No constraints — what style? What patterns?
- ❌ Generic, unusable output

**Talking point:** "This is what happens when we don't give enough information."

---

### 3. Prompt 2: Clear (3 min)

**Show the improved prompt:**

> "Generate an ASP.NET Core REST controller with one GET endpoint that returns a list of users"

**Run it and show the output.**

**Point out improvements:**

- ✅ Task: Generate
- ✅ What: REST controller
- ✅ Format: GET endpoint, returns list
- ✅ Better output, but still generic

**Talking point:** "Adding clarity helps, but we can do more."

---

### 4. Prompt 3: Clear + Context + Constraints (4 min)

**Show the refined prompt:**

> "Generate an ASP.NET Core 8 REST controller using C# 12. Use constructor injection (not service-locator style access), return ActionResult<List<UserDto>>, and include proper error handling. Use explicit DTO return types and clear null handling."

**Run it and show the output.**

**Point out improvements:**

- ✅ Stack: ASP.NET Core 8, C# 12
- ✅ Style: Constructor injection
- ✅ Patterns: ActionResult, error handling
- ✅ Constraints: No ambiguous nullable returns
- ✅ Much better, targeted output

**Talking point:** "Adding context and constraints makes the output match our needs."

---

### 5. Compare all three (2 min)

**Show side-by-side:**

- Prompt 1 (vague) → Generic output
- Prompt 2 (clear) → Better, but still generic
- Prompt 3 (clear + context + constraints) → Targeted, usable output

**Key lesson:**

- Clarity: What do you want?
- Context: What does the model need to know?
- Constraints: What are the rules?

**Bridge to next concept:** "Now let's see how adding examples (few-shot) can further improve output..."

---

## Teaching Tips

- **Emphasize**: Progressive building — start simple, add complexity
- **Watch for**: Delegates who think prompt 2 is "good enough" — show how prompt 3 is better
- **Adapt**: If time is short, skip prompt 2 and go straight from vague to constrained

---

## Time Allocation

- Set up problem: 1 min
- Prompt 1 (vague): 2 min
- Prompt 2 (clear): 3 min
- Prompt 3 (constrained): 4 min
- Compare: 2 min
- **Total: ~12 minutes**
