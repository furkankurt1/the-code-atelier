using Core.Entities;

namespace Entities.Concrete;

public class TaskItem : IEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; } // Foreign key
    public string Title { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public Project Project { get; set; }
}