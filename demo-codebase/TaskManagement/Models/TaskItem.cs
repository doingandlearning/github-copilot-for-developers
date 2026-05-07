namespace TaskManagement.Models;

public class TaskItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public int OwnerId { get; set; }
}

public enum Priority
{
    Low,
    Medium,
    High
}

public enum TaskStatus
{
    Todo,
    InProgress,
    Done
}
