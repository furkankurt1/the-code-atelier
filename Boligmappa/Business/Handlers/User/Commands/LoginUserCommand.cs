using Core.Abstract;
using Core.Utilities.ResultWrapper;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.User.Commands;

public class LoginUserCommand : IRequest<IDataResult<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, IDataResult<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenHelper _tokenHelper;

        public LoginUserCommandHandler(IUserRepository userRepository, IPasswordService passwordService, ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return new ErrorDataResult<string>("User not found.");

            if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                return new ErrorDataResult<string>("Incorrect password.");

            var token = _tokenHelper.CreateToken(user.Id, user.Email, user.Role);

            return new SuccessDataResult<string>(token, "Login successful.");
        }
    }
}