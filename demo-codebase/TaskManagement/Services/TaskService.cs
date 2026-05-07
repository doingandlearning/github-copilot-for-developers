using TaskManagement.DTOs;
using TaskManagement.Models;
using TaskManagement.Repositories;

namespace TaskManagement.Services;

public interface ITaskService
{
    Task<TaskResponse> CreateAsync(CreateTaskRequest request);
    Task<IEnumerable<TaskResponse>> GetByOwnerAsync(int ownerId, string? priority = null);
    Task<TaskResponse?> GetByIdAsync(int id);
    Task<TaskResponse?> UpdateAsync(int id, UpdateTaskRequest request);
    Task<bool> DeleteAsync(int id);
}

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Title is required", nameof(request));

        if (request.Title.Length > 100)
            throw new ArgumentException("Title cannot exceed 100 characters", nameof(request));

        if (!Enum.TryParse<Priority>(request.Priority, ignoreCase: true, out var priority))
            throw new ArgumentException($"Invalid priority: {request.Priority}", nameof(request));

        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Priority = priority,
            OwnerId = request.OwnerId
        };

        var saved = await _repository.SaveAsync(task);
        return MapToResponse(saved);
    }

    public async Task<IEnumerable<TaskResponse>> GetByOwnerAsync(int ownerId, string? priority = null)
    {
        Priority? priorityFilter = null;

        if (priority is not null)
        {
            if (!Enum.TryParse<Priority>(priority, ignoreCase: true, out var parsed))
                throw new ArgumentException($"Invalid priority: {priority}");
            priorityFilter = parsed;
        }

        var tasks = await _repository.GetByOwnerAsync(ownerId, priorityFilter);
        return tasks.Select(MapToResponse);
    }

    public async Task<TaskResponse?> GetByIdAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);
        return task is null ? null : MapToResponse(task);
    }

    // NOTE FOR DEMO: This method has a bug — it doesn't set CompletedAt when
    // status changes to Done. Good candidate for: /fix, or ask delegates to spot it
    public async Task<TaskResponse?> UpdateAsync(int id, UpdateTaskRequest request)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return null;

        if (request.Title is not null)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Title cannot be empty");
            existing.Title = request.Title;
        }

        if (request.Description is not null)
            existing.Description = request.Description;

        if (request.Priority is not null)
        {
            if (!Enum.TryParse<Priority>(request.Priority, ignoreCase: true, out var priority))
                throw new ArgumentException($"Invalid priority: {request.Priority}");
            existing.Priority = priority;
        }

        if (request.Status is not null)
        {
            if (!Enum.TryParse<Models.TaskStatus>(request.Status, ignoreCase: true, out var status))
                throw new ArgumentException($"Invalid status: {request.Status}");

            // BUG: CompletedAt should be set here when status == Done
            existing.Status = status;
        }

        var updated = await _repository.UpdateAsync(existing);
        return updated is null ? null : MapToResponse(updated);
    }

    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);

    private static TaskResponse MapToResponse(TaskItem task) => new(
        task.Id,
        task.Title,
        task.Description,
        task.Priority.ToString(),
        task.Status.ToString(),
        task.CreatedAt,
        task.CompletedAt,
        task.OwnerId
    );
}
