# Module 3 Demo Preparation Guide

**CRITICAL:** Module 3 is the anchor module. These demos must work.

---

## Pre-Demo Setup Checklist

### IDE Setup (Do This First)

**Create ASP.NET Core 8 Project:**

1. **Project Structure:**
   ```
   src/
   ├── Models/
   │   ├── Book.cs
   │   └── User.cs
   ├── Repositories/
   │   └── IBookRepository.cs
   └── Services/
       ├── BookService.cs
       └── UserService.cs
   
   tests/
   └── Services/
       ├── BookServiceTests.cs
       └── UserServiceTests.cs
   ```

2. **Dependencies** (`.csproj`):
   - ASP.NET Core Web API
   - Entity Framework Core
   - EF Core InMemory provider (for testing)
   - xUnit
   - Moq

3. **Classes to Create:**

   **Book.cs** - Use `code-samples/Book.cs` as template
   **User.cs** - Use `code-samples/User.cs` as template
   **IBookRepository.cs** - Interface with `FindByAuthorContainingIgnoreCase(string author)`
   **BookService.cs** - Service with `FindBooksByAuthor` method (see Demo 4)
   **UserService.cs** - Service with `GetActiveUserEmails` method (see Demo 3)

4. **Test Classes:**
   - **BookServiceTest.csharp** - Empty initially, will generate in Demo 4
   - **UserServiceTest.csharp** - Has test for getActiveUserEmails (see Demo 3)

5. **VERIFY:**
   - [ ] Project compiles
   - [ ] All classes exist
   - [ ] Tests compile (even if empty)
   - [ ] Can run tests in IDE

---

## Demo-by-Demo Preparation

### Demo 1: ASP.NET Core Generation

**What You Need:**
- [ ] AI chat tool open (GitHub Copilot for Business or approved tool)
- [ ] Prompts ready (from `prompts/01-spring-boot-generation-prompts.txt`)
- [ ] IDE open with ASP.NET Core project
- [ ] New package ready for generated code (or paste location)

**Test Beforehand:**
- [ ] Run Prompt 1 - Does it generate good entity?
- [ ] Run Prompt 2 - Does it generate repository + service?
- [ ] Run Prompt 3 - Does refinement work?
- [ ] Time yourself - Does it fit in 15 min?

**Backup:**
- [ ] Screenshots of all three prompts and outputs
- [ ] Generated code saved in files
- [ ] Can show slides if live fails

---

### Demo 2: Legacy Explanation

**What You Need:**
- [ ] AI chat tool open
- [ ] Legacy code snippet ready (from `code-samples/LegacyUserBean.csharp`)
- [ ] Prompts ready (from `prompts/02-legacy-explanation-prompts.txt`)

**Test Beforehand:**
- [ ] Run all 4 prompts with legacy code
- [ ] Verify explanations are helpful
- [ ] Time yourself - Does it fit in 15 min?

**Backup:**
- [ ] Screenshots of all 4 layers of explanation
- [ ] Pre-written explanations (if AI fails)
- [ ] Can show slides with explanations

---

### Demo 3: Refactoring (CRITICAL - Most Complex)

**What You Need:**
- [ ] IDE open with UserService class
- [ ] Original verbose method in UserService
- [ ] Test class with test that PASSES with original code
- [ ] AI chat tool open
- [ ] Prompt ready (from `prompts/03-refactoring-prompts.txt`)

**CRITICAL Setup Steps:**

1. **Create UserService.cs:**
   ```csharp
   public class UserService {
       public List<string> GetActiveUserEmails(List<User> users) {
           var emails = new List<string>();
           foreach (var user in users) {
               if (user is not null && user.IsActive && !string.IsNullOrWhiteSpace(user.Email)) {
                   emails.Add(user.Email);
                       }
           }
           return emails;
       }
   }
   ```

2. **Create UserServiceTest.csharp:**
   ```csharp
   public class UserServiceTests {
       private UserService service = new UserService();
       
       [Fact]
       public void GetActiveUserEmails_ReturnsOnlyActiveUsersWithEmail() {
           var activeUser = new User { Name = "John", Email = "john@example.com", IsActive = true };
           var inactiveUser = new User { Name = "Jane", Email = "jane@example.com", IsActive = false };
           var nullEmailUser = new User { Name = "Bob", Email = null, IsActive = true };
           
           var users = new List<User?> { activeUser, inactiveUser, nullEmailUser, null };
           
           var emails = service.GetActiveUserEmails(users!);
           
           Assert.Single(emails);
           Assert.Equal("john@example.com", emails[0]);
       }
   }
   ```

3. **VERIFY TEST PASSES** with original code

4. **Test Refactoring:**
   - Run prompt to generate refactored code
   - Replace method in IDE
   - Run test - does it still pass?
   - If not, refine prompt and try again

**Test Beforehand:**
- [ ] Original code compiles
- [ ] Test passes with original code
- [ ] Refactored code compiles
- [ ] Test passes with refactored code
- [ ] Time yourself - Does it fit in 15 min?

**Backup:**
- [ ] Screenshots of before/after code
- [ ] Screenshot of test passing before
- [ ] Screenshot of test passing after
- [ ] Can show slides if IDE fails

---

### Demo 4: Unit Testing (CRITICAL - High Tech Dependency)

**What You Need:**
- [ ] IDE open with BookService class
- [ ] BookService has findBooksByAuthor method
- [ ] IBookRepository interface exists
- [ ] xUnit and Moq configured
- [ ] AI chat tool open
- [ ] Prompts ready (from `prompts/04-unit-testing-prompts.txt`)

**CRITICAL Setup Steps:**

1. **Verify BookService exists:**
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

2. **Verify IBookRepository exists:**
   ```csharp
   public interface IBookRepository {
       List<Book> FindByAuthorContainingIgnoreCase(string author);
   }
   ```

3. **Create empty test class:**
   ```csharp
   public class BookServiceTests {
       // Will generate tests here
   }
   ```

**Test Beforehand:**
- [ ] Run Prompt 1 - Do generated tests compile?
- [ ] Do generated tests pass?
- [ ] If not, test Prompt 2 (refinement)
- [ ] Time yourself - Does it fit in 15 min?

**Backup:**
- [ ] Screenshots of generated tests
- [ ] Pre-written tests that work
- [ ] Can show slides if IDE fails

---

## Demo Execution Tips

### If Demo Goes Wrong:

**Demo 1 (Generation):**
- If output is poor → "This is why we review! Let me refine..."
- Show refinement process
- Emphasize iterative improvement

**Demo 2 (Explanation):**
- If explanation is wrong → "This is why we verify! Let me check..."
- Show how to verify explanation
- Emphasize explanation is starting point

**Demo 3 (Refactoring):**
- If test fails → "Perfect! This is why we test! Let me fix..."
- Show refinement process
- Emphasize safety-first approach

**Demo 4 (Testing):**
- If tests don't compile → "This happens! Let me refine..."
- Show refinement process
- Emphasize iterative test generation

### If Tech Fails:

- Stay calm
- Use backup screenshots
- Explain conceptually
- "Let me show you what should happen..."
- Move to next demo

---

## Time Management

**Total Demo Time:** ~60 minutes (4 demos × 15 min)

**If Running Behind:**
- Demo 1: Can trim to 10 min (skip refinement)
- Demo 2: Can trim to 10 min (skip risk analysis)
- Demo 3: Keep full - most important
- Demo 4: Can trim to 10 min (skip edge cases)

**If Running Ahead:**
- Add more examples
- Show more refinement rounds
- Let delegates try prompts themselves

---

## Success Criteria

**Demo 1 Success:**
- ✅ Shows generation is fast
- ✅ Shows refinement process
- ✅ Shows review checklist

**Demo 2 Success:**
- ✅ Shows layered explanation approach
- ✅ Shows how to understand legacy code
- ✅ Shows migration path

**Demo 3 Success:**
- ✅ Shows test-first refactoring
- ✅ Shows safety approach
- ✅ Shows before/after comparison

**Demo 4 Success:**
- ✅ Shows test generation
- ✅ Shows iterative refinement
- ✅ Shows running tests is essential

---

## Final Check Before Delivery

- [ ] All code compiles
- [ ] All tests pass
- [ ] All prompts tested
- [ ] All demos timed
- [ ] Backup materials ready
- [ ] IDE ready to show
- [ ] AI tool access confirmed

**You're ready!** 🚀
