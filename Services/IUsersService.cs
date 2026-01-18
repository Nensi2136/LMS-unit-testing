using LMS.Models;

namespace LMS.Services;

public interface IUsersService
{
    Task<List<UserDto>> GetUsersAsync();
    Task<UserDto?> GetUserAsync(int id);
    Task<bool> UpdateUserAsync(int id, User user);
    Task<UserDto> CreateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}
