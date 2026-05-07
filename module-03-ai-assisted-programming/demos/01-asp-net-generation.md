# Demo 1: ASP.NET Core generation — from spec to working code

## Goal

Show delegates how to generate a complete ASP.NET Core 8 application layer (entity, repository, service) from a simple specification, then refine iteratively.

**Teaching style**: Problem-first approach, progressive building

---

## Before the demo

- Ensure you have access to an AI chat tool (GitHub Copilot for Business or approved tool).
- Use only **synthetic** examples (no real internal code or APIs).
- Have an IDE ready to show the generated code can compile (optional but helpful).

---

## Steps

**Problem-first approach**: Start by showing the manual pain

### 1. Set up the problem (2 min)

**Say:** "Imagine you need to build a Book entity with Entity Framework Core annotations, a repository abstraction, and a service method. How long does this take you manually?"

**Show the pain:**
- ❌ Write entity class: 5 minutes
- ❌ Add Entity Framework Core annotations: 2 minutes
- ❌ Create repository abstraction: 3 minutes
- ❌ Write service class: 5 minutes
- ❌ **Total: 15+ minutes** of repetitive boilerplate

**Talking point:** "Let's see how AI can help generate this in seconds, then refine it to match our needs."

---

### 2. Show the initial prompt (2 min)

**Display the prompt** (have it ready to copy-paste from `prompts/01-spring-boot-generation-prompts.txt`):

> "Generate an ASP.NET Core 8 Entity Framework Core entity for a Book with the following fields:
> - id: long, primary key, auto-generated
> - title: string, required
> - author: string, required
> - isbn: string, unique, required
> - publishedYear: int
> Use C# 12 and include proper Entity Framework Core annotations."

**What to say:**
- "Notice the **clarity**: what we want (entity)"
- "Notice the **context**: ASP.NET Core 8, C# 12"
- "Notice the **constraints**: specific fields and annotations"
- "This follows the 3Cs from Module 2 — clarity, context, constraints"
- "Let's see what AI generates..."

---

### 3. Generate the entity (3 min)

**Run the prompt** (paste into AI chat) and show the output.

**What to say while waiting:**
- "I'm pasting this into [AI tool name]..."
- "Let's see what it generates..."

**When output appears, point out:**
- ✅ "Complete entity class — all fields included"
- ✅ "Proper Entity Framework Core mapping — `[Table]`, `[Key]`, `[DatabaseGenerated]`"
- ✅ "Field validations — `[Required]`, `[MaxLength]`"
- ✅ "C# 12 style — modern patterns"

**What to say:**
- "This took 30 seconds instead of 15 minutes of manual coding"
- "But let's check — does it match our needs? Do we need to refine it?"
- "Let's build on this — add repository and service"

---

### 4. Add repository and service (3 min)

**Refine the prompt** (from `prompts/01-spring-boot-generation-prompts.txt`):

> "Now generate a Entity Framework Core repository abstraction for this Book entity, and a service class with a method to find all books. Use constructor injection, not service-locator style injection. Return ActionResult<List<BookDto>>."

**What to say:**
- "Now I'm adding more context — repository and service"
- "Notice I'm specifying constructor injection — that's a constraint"
- "Let's see what it generates..."

**Run and show output.**

**Point out:**
- ✅ "Repository abstraction exposes query methods — correct pattern"
- ✅ "Service uses constructor injection — matches our constraint"
- ✅ "Proper return type — ActionResult<List<BookDto>>"

**What to say:**
- "We're building incrementally — entity first, then repository, then service"
- "This is progressive building — don't ask for everything at once"

---

### 5. Refine for team style (3 min)

**Show a potential issue:** Maybe the output uses nullable reference types or doesn't match team conventions.

**Refine prompt:**

> "Update the service method to not use nullable reference types for return types. Use ActionResult with proper HTTP status codes. Include error handling for empty results."

**Run and show improved output.**

**Point out:**
- ✅ No nullable reference types (matches team style)
- ✅ Proper HTTP status codes
- ✅ Error handling included

**Say:** "Iterative refinement — we review, identify issues, refine the prompt, and improve."

---

### 6. Review checklist (2 min)

**Show the evaluation checklist:**

- ✅ **Correctness:** Does it compile? (Show in IDE if possible)
- ✅ **Version:** ASP.NET Core 8? C# 12?
- ✅ **Style:** Constructor injection? No service-locator style injection?
- ✅ **Security:** Any obvious issues? (None in this case)
- ✅ **Tests:** Can we test this? (Yes — we'll generate tests in Demo 4)

**Say:** "Always review before pasting into your codebase."

---

## If you can't run live

- Show pre-prepared screenshots of the prompts and outputs.
- Use slides with the code examples.
- Emphasize the process: prompt → generate → review → refine → apply.

---

## Teaching Tips

- **Emphasize**: Generation is fast, but review is essential
- **Watch for**: Delegates who want to paste without review — show the checklist
- **Adapt**: If generation feels too abstract, show more examples or let delegates try themselves

---

## Time Allocation

- Set up problem: 2 min
- Show initial prompt: 2 min
- Generate entity: 3 min
- Add repository/service: 3 min
- Refine for style: 3 min
- Review checklist: 2 min
- **Total: ~15 minutes**
