// ITaskRepository.cs
using Core.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ITaskRepository : IEntityRepository<TaskItem>
{
    Task<IEnumerable<TaskItem>> GetAllByProjectIdAsync(int projectId);
}