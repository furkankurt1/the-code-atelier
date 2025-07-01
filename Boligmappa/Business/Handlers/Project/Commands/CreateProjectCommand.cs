using Core.Utilities.ResultWrapper;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.Project.Commands;

public class CreateProjectCommand : IRequest<IResult>
{
    public string Title { get; set; }
    public string Description { get; set; }

    public int CreatedBy { get; private set; }

    public void SetCreatedBy(int userId)
    {
        CreatedBy = userId;
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, IResult>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IResult> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var newProject = new Entities.Concrete.Project
            {
                Title = request.Title,
                Description = request.Description,
                CreatedBy = request.CreatedBy
            };

            await _projectRepository.AddAsync(newProject);
            await _projectRepository.SaveChangesAsync();

            return new SuccessResult("Project created successfully.");
        }
    }
}