namespace LMS.Services;

public interface IUserService
{
    string GetUserFullName(int userId);
    bool IsUserActive(int userId);
}
