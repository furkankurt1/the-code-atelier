using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class UserRepository : EfEntityRepositoryBase<User, AppDbContext>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await Context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}