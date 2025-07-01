using Entities.Abstract;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } 
        public string PasswordHash { get; set; } 
        public string Role { get; set; }
    }
}