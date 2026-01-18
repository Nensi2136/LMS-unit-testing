using LMS.Data;
using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services;

public class UsersService : IUsersService
{
    private readonly AppDbContext _context;

    public UsersService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users.Select(ToDto).ToList();
    }

    public async Task<UserDto?> GetUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null ? null : ToDto(user);
    }

    public async Task<bool> UpdateUserAsync(int id, User user)
    {
        if (id != user.UId)
        {
            throw new ArgumentException("Route id does not match entity id", nameof(id));
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            var exists = await _context.Users.AnyAsync(e => e.UId == id);
            if (!exists)
            {
                return false;
            }

            throw;
        }
    }

    public async Task<UserDto> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return ToDto(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    private static UserDto ToDto(User u)
    {
        return new UserDto
        {
            UId = u.UId,
            UName = u.UName,
            UEmail = u.UEmail,
            UPhonenumber = u.UPhonenumber,
            IsAdmin = u.IsAdmin,
            CreatedAt = u.CreatedAt,
            UpdatedAt = u.UpdatedAt
        };
    }
}
