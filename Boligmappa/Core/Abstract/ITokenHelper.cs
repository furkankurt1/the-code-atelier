namespace Core.Abstract;

public interface ITokenHelper
{
    string CreateToken(int userId, string email, string role);
}