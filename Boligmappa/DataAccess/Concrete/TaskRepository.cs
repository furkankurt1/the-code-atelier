// TaskRepository.cs
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using DataAccess;

namespace DataAccess.Concrete;

public class TaskRepository : EfEntityRepositoryBase<TaskItem, AppDbContext>, ITaskRepository
{
    public TaskRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TaskItem>> GetAllByProjectIdAsync(int projectId)
    {
        return await Task.FromResult(Context.Set<TaskItem>().Where(t => t.ProjectId == projectId).ToList());
    }
}