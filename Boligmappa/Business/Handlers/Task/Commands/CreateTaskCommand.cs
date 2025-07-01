using Core.Utilities.ResultWrapper;
using DataAccess.Abstract;
using MediatR;
using Entities.Concrete;

namespace Business.Handlers.Task.Commands
{
    public class CreateTaskCommand : IRequest<IResult>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }

        public int UserId { get; private set; }
        public string Role { get; private set; }

        public void SetUserInfo(int userId, string role)
        {
            UserId = userId;
            Role = role;
        }

        public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, IResult>
        {
            private readonly ITaskRepository _taskRepository;
            private readonly IProjectRepository _projectRepository;

            public CreateTaskCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
            {
                _taskRepository = taskRepository;
                _projectRepository = projectRepository;
            }

            public async Task<IResult> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
            {
                if (request.Role != "Admin")
                {
                    var project = await _projectRepository.GetByIdAsync(request.ProjectId);
                    if (project == null)
                        return new ErrorResult("Project not found.");

                    if (project.CreatedBy != request.UserId)
                        return new ErrorResult("You can only add tasks to your own projects.");
                }

                var taskItem = new TaskItem
                {
                    Title = request.Title,
                    Description = request.Description,
                    ProjectId = request.ProjectId
                };

                await _taskRepository.AddAsync(taskItem);
                await _taskRepository.SaveChangesAsync();

                return new SuccessResult("Task created successfully.");
            }
        }
    }
}
