using Core.Entities;

namespace Entities.Concrete;

public class Project : IEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}