using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class ProjectRepository : EfEntityRepositoryBase<Project, AppDbContext>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context) { }
    }
}