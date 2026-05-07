using TaskManagement.Models;

namespace TaskManagement.Repositories;

// NOTE FOR DEMO: This is an in-memory implementation so the project runs
// without a database. In a real project this would use EF Core.
// Good candidate for: /explain, inline completion of missing methods
public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public Task<TaskItem?> GetByIdAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task<IEnumerable<TaskItem>> GetByOwnerAsync(int ownerId, Priority? priority = null)
    {
        var query = _tasks.Where(t => t.OwnerId == ownerId);

        if (priority.HasValue)
            query = query.Where(t => t.Priority == priority.Value);

        return Task.FromResult<IEnumerable<TaskItem>>(query.OrderByDescending(t => t.CreatedAt));
    }

    public Task<TaskItem> SaveAsync(TaskItem task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
        return Task.FromResult(task);
    }

    public Task<TaskItem?> UpdateAsync(TaskItem task)
    {
        var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existing is null) return Task.FromResult<TaskItem?>(null);

        _tasks.Remove(existing);
        _tasks.Add(task);
        return Task.FromResult<TaskItem?>(task);
    }

    // TODO: implement DeleteAsync
    // Good demo: ask Copilot inline to complete this
    public Task<bool> DeleteAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return Task.FromResult(false);

        _tasks.Remove(task);
        return Task.FromResult(true);
    }
}
