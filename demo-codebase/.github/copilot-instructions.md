# GitHub Copilot Custom Instructions

## Stack
- ASP.NET Core 8, C# 12
- xUnit for tests, Moq for mocking
- FluentValidation for request validation
- In-memory repository (no database — this is a demo project)

## Coding conventions
- Use constructor injection throughout
- Repository pattern: controllers → services → repositories
- Never put business logic in controllers
- Use record types for DTOs
- Async all the way down — no `.Result` or `.Wait()`
- Use `required` keyword for non-nullable properties with no default

## Naming
- System under test in tests: `_sut`
- Test method names: `MethodName_Condition_ExpectedBehaviour`
- Private fields: `_camelCase`

## Error handling
- Services throw `ArgumentException` for invalid input
- Controllers catch and return `BadRequest` with `{ error: message }`
- Return `NotFound()` for missing resources, never throw

## Tests
- One assertion per test where possible
- Use `It.IsAny<T>()` sparingly — prefer specific matchers
- Always verify repository calls with `.Verify()` when testing side effects
