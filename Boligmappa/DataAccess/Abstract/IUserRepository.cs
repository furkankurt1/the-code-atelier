using Core.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IUserRepository : IEntityRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}