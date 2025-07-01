using Core.Utilities.ResultWrapper;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Project.Queries
{
    public class GetProjectsQuery : IRequest<IDataResult<List<Entities.Concrete.Project>>>
    {
        public int UserId { get; set; }

        public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IDataResult<List<Entities.Concrete.Project>>>
        {
            private readonly IProjectRepository _projectRepository;
            private readonly IUserRepository _userRepository;

            public GetProjectsQueryHandler(IProjectRepository projectRepository, IUserRepository userRepository)
            {
                _projectRepository = projectRepository;
                _userRepository = userRepository;
            }

            public async Task<IDataResult<List<Entities.Concrete.Project>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null)
                    return new ErrorDataResult<List<Entities.Concrete.Project>>("User not found.");

                List<Entities.Concrete.Project> projects;

                if (user.Role == "Admin")
                {
                    projects = (await _projectRepository.GetAllAsync()).ToList();
                }
                else
                {
                    projects = (await _projectRepository.GetAllAsync(p => p.CreatedBy == request.UserId)).ToList();
                }

                return new SuccessDataResult<List<Entities.Concrete.Project>>(projects);
            }
        }
    }
}