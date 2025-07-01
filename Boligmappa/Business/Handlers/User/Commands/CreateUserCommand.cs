using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Abstract;
using Core.Utilities;
using Core.Utilities.ResultWrapper;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.User.Commands;

public class CreateUserCommand : IRequest<IResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await BusinessRules.RunAsync(
                CheckIfRoleIsValidAsync(request.Role),
                CheckIfEmailIsProvidedAsync(request.Email),
                CheckIfPasswordIsProvidedAsync(request.Password),
                CheckIfEmailAlreadyExistsAsync(request.Email)
            );

            if (result != null)
                return result;

            _passwordService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new Entities.Concrete.User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role
            };

            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();

            return new SuccessResult("User created successfully.");
        }

        #region Validation Rules (Async)

        private Task<IResult> CheckIfRoleIsValidAsync(string role)
        {
            string[] allowedRoles = { "Admin", "User" };
            return Task.FromResult<IResult>(
                allowedRoles.Contains(role)
                    ? new SuccessResult()
                    : new ErrorResult("Role must be either 'Admin' or 'User'."));
        }

        private Task<IResult> CheckIfEmailIsProvidedAsync(string email)
        {
            return Task.FromResult<IResult>(
                string.IsNullOrWhiteSpace(email)
                    ? new ErrorResult("Email is required.")
                    : new SuccessResult());
        }

        private Task<IResult> CheckIfPasswordIsProvidedAsync(string password)
        {
            return Task.FromResult<IResult>(
                string.IsNullOrWhiteSpace(password)
                    ? new ErrorResult("Password is required.")
                    : new SuccessResult());
        }

        #endregion

        #region Business Rules (Async)

        private async Task<IResult> CheckIfEmailAlreadyExistsAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user == null
                ? new SuccessResult()
                : new ErrorResult("A user with this email already exists.");
        }

        #endregion
    }
}