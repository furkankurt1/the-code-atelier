using Core.Utilities.ResultWrapper;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Project.Commands
{
    public class UpdateProjectCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; private set; }

        public void SetUserId(int userId)
        {
            UserId = userId;
        }

        public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, IResult>
        {
            private readonly IProjectRepository _projectRepository;
            private readonly IUserRepository _userRepository;

            public UpdateProjectCommandHandler(IProjectRepository projectRepository, IUserRepository userRepository)
            {
                _projectRepository = projectRepository;
                _userRepository = userRepository;
            }

            public async Task<IResult> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _projectRepository.GetByIdAsync(request.Id);
                if (project == null)
                    return new ErrorResult("Project not found.");

                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null)
                    return new ErrorResult("User not found.");

                var ruleCheck = OnlyOwnerOrAdminCanModify(user, project);
                if (!ruleCheck.Success)
                    return ruleCheck;

                project.Title = request.Title;
                project.Description = request.Description;

                await _projectRepository.SaveChangesAsync();
                return new SuccessResult("Project updated successfully.");
            }

            #region Business Rules

            private IResult OnlyOwnerOrAdminCanModify(Entities.Concrete.User user, Entities.Concrete.Project project)
            {
                if (user.Role == "Admin")
                    return new SuccessResult();

                if (project.CreatedBy != user.Id)
                    return new ErrorResult("You are not authorized to update this project.");

                return new SuccessResult();
            }

            #endregion
        }
    }
}