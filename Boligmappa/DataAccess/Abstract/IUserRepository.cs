using Core.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}