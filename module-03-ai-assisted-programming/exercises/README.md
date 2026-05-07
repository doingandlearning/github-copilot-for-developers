# Module 3 — Exercises

**For delegates.** Complete these during or after the Module 3 session. You can do them on your own or in pairs.

---

## Objective

By completing these exercises, you will:

- Generate ASP.NET Core 8 code (entities, repositories, services) with AI
- Use AI to explain legacy code and plan modernization
- Refactor code safely with AI assistance (with tests)
- Generate and refine unit tests with AI
- Evaluate AI output before applying it to your codebase

---

## Scenario

You're working on a **Library Management System** — an ASP.NET Core 8 application for managing books, authors, and loans. These exercises will help you practice using AI for code generation, explanation, refactoring, and testing.

**Remember:** Use only synthetic examples or generic ASP.NET Core patterns. Don't paste proprietary code or internal APIs.

---

## Exercise 1: Generate a complete entity layer

**Your task:**

Generate a complete entity layer for a **Loan** entity using AI:

1. **Entity:** Loan with fields:
   - id: long (primary key, auto-generated)
   - bookId: long (foreign key to Book)
   - userId: long (foreign key to User)
   - loanDate: DateOnly
   - returnDate: DateOnly? (nullable)
   - status: string (enum-like: "ACTIVE", "RETURNED", "OVERDUE")

2. **Repository:** Entity Framework Core repository abstraction for Loan

3. **Service:** Service class with method `FindActiveLoans()` that returns all loans with status "ACTIVE"

**Requirements:**

- Use ASP.NET Core 8 and C# 12
- Use constructor injection (not service-locator style access)
- Include proper Entity Framework Core annotations
- Follow ASP.NET Core best practices

**Hints:**

- Start with the entity, then add repository, then service
- Be specific about ASP.NET Core version and C# version
- Specify constructor injection in your prompt
- Review the output before considering it done

**Then:** Review the generated code using the evaluation checklist:
- ✅ Does it compile?
- ✅ Right ASP.NET Core/C# version?
- ✅ Matches your style (constructor injection)?
- ✅ Any security issues?

**Time:** About 15 minutes.

<details>
<summary>Example Solution for Exercise 1</summary>

**Example prompt:**

> "Generate an ASP.NET Core 8 Entity Framework Core entity for a Loan with the following fields:
> - id: long, primary key, auto-generated
> - bookId: long, foreign key reference
> - userId: long, foreign key reference
> - loanDate: DateOnly, required
> - returnDate: DateOnly?, nullable
> - status: string, required (values: ACTIVE, RETURNED, OVERDUE)
> Use C# 12 and include proper Entity Framework Core annotations. Then generate an Entity Framework Core repository abstraction for this entity, and a service class with a method FindActiveLoans() that returns all loans with status 'ACTIVE'. Use constructor injection."

**Expected output:** Complete entity, repository, and service with proper annotations and constructor injection.

**Review checklist:**
- ✅ Compiles
- ✅ ASP.NET Core 8, C# 12
- ✅ Constructor injection used
- ✅ Proper Entity Framework Core annotations

</details>

---

## Exercise 2: Explain legacy code

**Your task:**

Use AI to explain this legacy code snippet:

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

**Your prompts should cover:**

1. **What does this do?** — Functional explanation
2. **Why might it be written this way?** — Historical context
3. **What's the modern equivalent?** — ASP.NET Core 8 / C# 12 version
4. **What are the risks if I refactor it?** — Dependencies and breaking changes

**Hints:**

- Ask one question at a time (layered approach)
- Build from "what" to "how to modernize"
- Use the explanation to plan refactoring (Exercise 3)

**Then:** Write a brief summary (2-3 sentences) of what you learned about this code.

**Time:** About 10 minutes.

<details>
<summary>Example Solution for Exercise 2</summary>

**Example prompts:**

**Layer 1:**
> "Explain what this C# code does step by step. Break down each part and explain the functionality."

**Layer 2:**
> "Why might this code have been written this way? What patterns does it use? What was common at the time?"

**Layer 3:**
> "What would be the modern ASP.NET Core 8 / C# 12 equivalent of this code? Show a simple example."

**Layer 4:**
> "What are the risks or dependencies if I try to refactor this code? What might break?"

**Expected insights:**
- Functional: Processes pending orders and calculates totals per customer
- Historical: Uses old C# patterns (indexed loops, manual null checks)
- Modern: Could use LINQ, nullable reference types, better structure
- Risks: Depends on order status values, customer ID format, etc.

</details>

---

## Exercise 3: Refactor safely (with tests)

**Your task:**

Refactor the legacy code from Exercise 2 using AI, but **safely**:

1. **First:** Write a simple test for the existing code (or use AI to generate one)
2. **Run the test** — it should pass
3. **Refactor** the code using AI to modern C# 12 style (LINQ, nullable reference types, better structure)
4. **Run the test again** — it should still pass
5. **Review the diff** — what changed? Is behavior the same?

**Requirements:**

- Use C# 12 LINQ
- Improve readability
- Maintain exact same behavior
- Use proper null-safety

**Hints:**

- Start with: "Generate a xUnit test for this method..."
- Then: "Refactor this method to use C# 12 LINQ..."
- Always run tests before and after
- Review the diff carefully

**Then:** Write a brief reflection: What changed? Was the refactoring safe? What would you do differently?

**Time:** About 20 minutes.

<details>
<summary>Example Solution for Exercise 3</summary>

**Step 1: Generate test**

> "Generate a xUnit test for this method. Cover happy path, null orders, and empty list scenarios."

**Step 2: Refactor**

> "Refactor this method to use C# 12 LINQ. Replace loops with LINQ, use nullable reference types for null-safety, and improve readability. Maintain the exact same behavior."

**Refactored version:**

```csharp
public void ProcessOrders()
{
    orders
        .Where(order => order is not null)
        .Where(order => order!.Status == "PENDING")
        .Where(order => !string.IsNullOrWhiteSpace(order.CustomerId))
        .ToList()
        .ForEach(order =>
        {
            totals[order.CustomerId!] =
                totals.GetValueOrDefault(order.CustomerId!, 0m) + order.Amount;
        });
}
```

**Key changes:**
- LINQ instead of manual iteration
- Functional style
- merge() instead of manual null check
- More readable

**Safety check:**
- ✅ Tests pass before
- ✅ Tests pass after
- ✅ Behavior unchanged
- ✅ More maintainable

</details>

---

## Exercise 4: Generate and refine unit tests

**Your task:**

Generate comprehensive unit tests for this service method:

```csharp
public class BookService {
    private readonly IBookRepository repository;
    
    public BookService(IBookRepository repository) {
        this.repository = repository;
    }
    
    public Book? FindBookByIsbn(string isbn) {
        if (string.IsNullOrWhiteSpace(isbn)) {
            return null;
        }
        return repository.FindByIsbn(isbn);
    }
}
```

**Requirements:**

1. **Generate tests** using AI (xUnit, Moq)
2. **Cover:** Happy path, null input, empty string, book not found
3. **Run the tests** — do they pass?
4. **If tests fail:** Refine the prompt and fix them
5. **Add one edge case** (e.g., whitespace-only ISBN, repository throws exception)

**Hints:**

- Specify xUnit and Moq in your prompt
- Ask for specific scenarios (null, empty, not found)
- Run tests immediately — don't assume they work
- Iterate if they fail

**Then:** Write a brief summary: How many tests did you generate? Did they all pass? What did you refine?

**Time:** About 15 minutes.

<details>
<summary>Example Solution for Exercise 4</summary>

**Initial prompt:**

> "Generate xUnit and Moq tests for this service method. Cover happy path, null input, empty string, and book not found scenarios. Use C# 12."

**Generated tests (example):**

```csharp
public class BookServiceTests
{
    private readonly Mock<IBookRepository> repository = new();
    private readonly BookService service;

    public BookServiceTests()
    {
        service = new BookService(repository.Object);
    }

    [Fact]
    public void FindBookByIsbn_HappyPath_ReturnsBook()
    {
        var isbn = "1234567890";
        var book = new Book { Id = 1, Title = "Test Book", Author = "Author", Isbn = isbn, PublishedYear = 2023 };

        repository.Setup(r => r.FindByIsbn(isbn)).Returns(book);

        var result = service.FindBookByIsbn(isbn);

        Assert.NotNull(result);
        Assert.Equal("Test Book", result!.Title);
        repository.Verify(r => r.FindByIsbn(isbn), Times.Once);
    }

    [Fact]
    public void FindBookByIsbn_NullInput_ReturnsNull()
    {
        var result = service.FindBookByIsbn(null!);

        Assert.Null(result);
        repository.Verify(r => r.FindByIsbn(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void FindBookByIsbn_EmptyInput_ReturnsNull()
    {
        var result = service.FindBookByIsbn("");

        Assert.Null(result);
        repository.Verify(r => r.FindByIsbn(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void FindBookByIsbn_NotFound_ReturnsNull()
    {
        var isbn = "9999999999";
        repository.Setup(r => r.FindByIsbn(isbn)).Returns((Book?)null);

        var result = service.FindBookByIsbn(isbn);

        Assert.Null(result);
    }
}
```

**Refinement (if needed):**

> "Add a test for whitespace-only ISBN input and a test for when repository throws an exception."

**Key learning:**
- Generate → Run → Refine → Verify
- Don't trust tests without running them
- Iterative process

</details>

---

## Final Deliverables

Before moving to Module 4, ensure you have:

- [ ] Generated a complete entity layer (entity, repository, service) with AI (Exercise 1)
- [ ] Explained legacy code using layered prompts (Exercise 2)
- [ ] Refactored code safely with tests (Exercise 3)
- [ ] Generated and refined unit tests (Exercise 4)
- [ ] Reviewed all AI output using the evaluation checklist

---

## Extensions (nullable reference types)

If you finish early or want to go deeper:

1. **Generate a REST controller:** Use AI to generate a REST controller for your Loan entity with CRUD endpoints. Review it carefully.

2. **Explain a real method:** Find a method in your codebase (or use a provided example) that you don't fully understand. Use AI to explain it, then verify the explanation by reading the code yourself.

3. **Refactor with multiple strategies:** Take one method and refactor it using multiple AI prompts: (1) extract methods, (2) use LINQ, (3) improve naming. Compare the results.

4. **Generate integration tests:** Use AI to generate integration tests (not just unit tests) for your service. What's different about integration test prompts?

5. **Create a prompt library:** Document your best prompts for generation, explanation, refactoring, and testing. Share with your team.

---

## Key Learning Points

- **Generation:** Fast and consistent for boilerplate — but always review
- **Explanation:** Layered approach (what → why → modern → risks) builds understanding
- **Refactoring:** Safe when you have tests — test before, refactor, test after
- **Testing:** Generate tests, but always run and refine — iterative process
- **Review:** Always evaluate before applying — correctness, version, style, security

---

## After the exercises

- Keep your **best prompts** as templates for future use
- In Module 4 we'll cover **tooling strategies** — IDE integrations and workflow optimization
- Practice makes perfect — keep generating, explaining, refactoring, and testing with AI
- Remember: AI assists, but you're responsible for the code — always review and test
