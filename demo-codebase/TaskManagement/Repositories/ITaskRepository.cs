using TaskManagement.Models;

namespace TaskManagement.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(int id);
    Task<IEnumerable<TaskItem>> GetByOwnerAsync(int ownerId, Priority? priority = null);
    Task<TaskItem> SaveAsync(TaskItem task);
    Task<TaskItem?> UpdateAsync(TaskItem task);
    Task<bool> DeleteAsync(int id);
}
