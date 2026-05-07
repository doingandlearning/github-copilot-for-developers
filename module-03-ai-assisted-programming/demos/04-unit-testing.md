# Demo 4: Unit test generation — tests that work

## Goal

Show delegates how to generate xUnit/Moq tests for a service method, run them, and refine iteratively when tests fail.

**Teaching style**: Problem-first approach, progressive building

---

## Before the demo

- Ensure you have access to an AI chat tool.
- Have an IDE ready with ASP.NET Core project, xUnit, and Moq.
- Prepare a sample service method to test (synthetic example provided).
- Use only **synthetic** examples.

---

## Steps

**Problem-first approach**: Start with manual test writing pain

### 1. Set up the problem (2 min)

**Say:** "You need to write tests for this service method. How long does it take you to write a comprehensive test manually?"

**Show the service method** (example provided below).

**Show the pain:**
- ❌ Write test class: 2 minutes
- ❌ Set up mocks: 3 minutes
- ❌ Write happy path test: 3 minutes
- ❌ Write edge case tests: 5 minutes
- ❌ **Total: 13+ minutes** per method

**Talking point:** "Let's see how AI can generate comprehensive tests in seconds, then we'll refine them."

---

### 2. Generate basic tests (3 min)

**Show the prompt:**

> "Generate xUnit and Moq tests for this service method. Cover the happy path, null input, and empty list scenarios. Use C# 12."

**Include the service method:**

```csharp
public class BookService {
    private readonly IBookRepository repository;
    
    public BookService(IBookRepository repository) {
        this.repository = repository;
    }
    
    public List<Book> FindBooksByAuthor(string author) {
        if (string.IsNullOrWhiteSpace(author)) {
            return new List<Book>();
        }
        return repository.FindByAuthorContainingIgnoreCase(author);
    }
}
```

**Run the prompt** and show the generated tests.

**Point out:**
- ✅ Test class structure
- ✅ Mock setup
- ✅ Multiple test cases (happy path, null, empty)

---

### 3. Run the tests (3 min)

**Show the generated tests** in the IDE.

**Say:** "Let's run these tests and see if they work."

**Run the tests.**

**If they pass:**
- ✅ "Great! But let's add more edge cases."

**If they fail:**
- Show the failure: "One test failed. Let's see why."
- Diagnose: "The mock isn't set up correctly for the empty string case."
- **This is the key learning moment** — show iterative refinement.

---

### 4. Refine the tests (3 min)

**If tests failed, show refinement:**

**Refine prompt:**

> "Fix the test for empty string input. The service should return an empty list when author is null or empty. Update the mock setup accordingly."

**Run the refined prompt** and show improved tests.

**Re-run tests** — they should pass now.

**Say:** "Test generation is iterative — generate, run, refine, repeat."

---

### 5. Add more edge cases (2 min)

**Refine prompt:**

> "Add tests for edge cases: author with only whitespace, author not found (empty result), and repository throws exception."

**Run and show additional tests.**

**Point out:**
- ✅ More comprehensive coverage
- ✅ Exception handling tested
- ✅ Edge cases covered

**Say:** "Progressive building — start with basics, add complexity."

---

### 6. Test quality checklist (2 min)

**Show the test quality checklist:**

- ✅ **Coverage:** Happy path, edge cases, exceptions?
- ✅ **Mocks:** Properly set up?
- ✅ **Assertions:** Clear and specific?
- ✅ **Run:** Do tests actually pass?

**Say:** "Generate tests, but always run and verify. Don't trust tests without running them."

---

## Sample service method to test

**BookService method** (synthetic):

```csharp
public class BookService {
    private readonly IBookRepository repository;
    
    public BookService(IBookRepository repository) {
        this.repository = repository;
    }
    
    public List<Book> FindBooksByAuthor(string author) {
        if (string.IsNullOrWhiteSpace(author)) {
            return new List<Book>();
        }
        return repository.FindByAuthorContainingIgnoreCase(author);
    }
}
```

**Expected generated tests** (what AI should generate):

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
    public void FindBooksByAuthor_HappyPath()
    {
        var author = "Tolkien";
        var expectedBooks = new List<Book>
        {
            new() { Id = 1, Title = "The Hobbit", Author = "Tolkien", Isbn = "123", PublishedYear = 1937 },
            new() { Id = 2, Title = "The Lord of the Rings", Author = "Tolkien", Isbn = "456", PublishedYear = 1954 }
        };

        repository.Setup(r => r.FindByAuthorContainingIgnoreCase(author)).Returns(expectedBooks);

        var result = service.FindBooksByAuthor(author);

        Assert.Equal(2, result.Count);
        Assert.Equal("Tolkien", result[0].Author);
        repository.Verify(r => r.FindByAuthorContainingIgnoreCase(author), Times.Once);
    }

    [Fact]
    public void FindBooksByAuthor_NullInput()
    {
        var result = service.FindBooksByAuthor(null!);

        Assert.Empty(result);
        repository.Verify(r => r.FindByAuthorContainingIgnoreCase(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void FindBooksByAuthor_EmptyInput()
    {
        var result = service.FindBooksByAuthor(string.Empty);

        Assert.Empty(result);
        repository.Verify(r => r.FindByAuthorContainingIgnoreCase(It.IsAny<string>()), Times.Never);
    }
}
```

---

## If you can't run live

- Show pre-prepared screenshots of generated tests.
- Use slides with test examples.
- Emphasize the process: generate → run → refine → verify.

---

## Teaching Tips

- **Emphasize**: Generate tests, but always run them — AI can generate tests that don't compile or don't test the right thing
- **Watch for**: Delegates who trust tests without running — show a failure example
- **Adapt**: If test generation fails, show iterative refinement process

---

## Time Allocation

- Set up problem: 2 min
- Generate basic tests: 3 min
- Run the tests: 3 min
- Refine the tests: 3 min
- Add edge cases: 2 min
- Test quality checklist: 2 min
- **Total: ~15 minutes**
