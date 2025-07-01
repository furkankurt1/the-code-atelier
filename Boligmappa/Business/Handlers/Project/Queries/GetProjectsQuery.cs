using Core.Utilities.ResultWrapper;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Project.Queries;

public class GetProjectsQuery : IRequest<IDataResult<List<Entities.Concrete.Project>>>
{
    public int UserId { get; set; }

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IDataResult<List<Entities.Concrete.Project>>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IDataResult<List<Entities.Concrete.Project>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAllAsync(p => p.CreatedBy == request.UserId);
            return new SuccessDataResult<List<Entities.Concrete.Project>>(projects);
        }
    }
}