using Entities.Abstract;

namespace Entities.Concrete
{
    public class TaskItem : IEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}