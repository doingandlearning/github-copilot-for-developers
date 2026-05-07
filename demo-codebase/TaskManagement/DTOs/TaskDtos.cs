namespace TaskManagement.DTOs;

public record CreateTaskRequest(
    string Title,
    string? Description,
    string Priority,
    int OwnerId
);

public record UpdateTaskRequest(
    string? Title,
    string? Description,
    string? Priority,
    string? Status
);

public record TaskResponse(
    int Id,
    string Title,
    string? Description,
    string Priority,
    string Status,
    DateTime CreatedAt,
    DateTime? CompletedAt,
    int OwnerId
);
