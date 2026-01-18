using LMS.Controllers;
using LMS.Models;
using LMS.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LMS.Tests;

public class UsersControllerTests
{
    [Fact]
    public async Task GetUsers_ReturnsUsers()
    {
        var expected = new List<UserDto>
        {
            new UserDto { UId = 1, UName = "A", UEmail = "a@a.com", UPhonenumber = "1", IsAdmin = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new UserDto { UId = 2, UName = "B", UEmail = "b@b.com", UPhonenumber = "2", IsAdmin = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        };

        var usersService = new Mock<IUsersService>(MockBehavior.Strict);
        usersService.Setup(s => s.GetUsersAsync()).ReturnsAsync(expected);

        var controller = new UsersController(usersService.Object);

        var result = await controller.GetUsers();

        Assert.NotNull(result);
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Count());
        Assert.Equal(expected[0].UId, result.Value.First().UId);
        usersService.VerifyAll();
    }

    [Fact]
    public async Task GetUser_WhenNotFound_ReturnsNotFound()
    {
        var usersService = new Mock<IUsersService>(MockBehavior.Strict);
        usersService.Setup(s => s.GetUserAsync(123)).ReturnsAsync((UserDto?)null);

        var controller = new UsersController(usersService.Object);

        var result = await controller.GetUser(123);

        Assert.IsType<NotFoundResult>(result.Result);
        usersService.VerifyAll();
    }

    [Fact]
    public async Task GetUser_WhenFound_ReturnsDto()
    {
        var expected = new UserDto { UId = 5, UName = "X", UEmail = "x@x.com", UPhonenumber = "999", IsAdmin = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

        var usersService = new Mock<IUsersService>(MockBehavior.Strict);
        usersService.Setup(s => s.GetUserAsync(5)).ReturnsAsync(expected);

        var controller = new UsersController(usersService.Object);

        var result = await controller.GetUser(5);

        Assert.NotNull(result.Value);
        Assert.Equal(expected.UId, result.Value.UId);
        usersService.VerifyAll();
    }

    [Fact]
    public async Task PostUser_ReturnsCreatedAtAction_WithDto()
    {
        var user = new User { UId = 10, UName = "N", UEmail = "n@n.com", UPhonenumber = "123", IsAdmin = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
        var expectedDto = new UserDto { UId = 10, UName = "N", UEmail = "n@n.com", UPhonenumber = "123", IsAdmin = false, CreatedAt = user.CreatedAt, UpdatedAt = user.UpdatedAt };

        var usersService = new Mock<IUsersService>(MockBehavior.Strict);
        usersService.Setup(s => s.CreateUserAsync(user)).ReturnsAsync(expectedDto);

        var controller = new UsersController(usersService.Object);

        var result = await controller.PostUser(user);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetUser", created.ActionName);
        var body = Assert.IsType<UserDto>(created.Value);
        Assert.Equal(expectedDto.UId, body.UId);
        usersService.VerifyAll();
    }

    [Fact]
    public async Task PutUser_WhenIdMismatch_ReturnsBadRequest()
    {
        var usersService = new Mock<IUsersService>(MockBehavior.Strict);
        var controller = new UsersController(usersService.Object);

        var result = await controller.PutUser(1, new User { UId = 2 });

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task PutUser_WhenNotFound_ReturnsNotFound()
    {
        var user = new User { UId = 7, UName = "U", UEmail = "u@u.com", UPhonenumber = "7", IsAdmin = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

        var usersService = new Mock<IUsersService>(MockBehavior.Strict);
        usersService.Setup(s => s.UpdateUserAsync(7, user)).ReturnsAsync(false);

        var controller = new UsersController(usersService.Object);

        var result = await controller.PutUser(7, user);

        Assert.IsType<NotFoundResult>(result);
        usersService.VerifyAll();
    }

    [Fact]
    public async Task PutUser_WhenUpdated_ReturnsNoContent()
    {
        var user = new User { UId = 7, UName = "U", UEmail = "u@u.com", UPhonenumber = "7", IsAdmin = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

        var usersService = new Mock<IUsersService>(MockBehavior.Strict);
        usersService.Setup(s => s.UpdateUserAsync(7, user)).ReturnsAsync(true);

        var controller = new UsersController(usersService.Object);

        var result = await controller.PutUser(7, user);

        Assert.IsType<NoContentResult>(result);
        usersService.VerifyAll();
    }

    [Fact]
    public async Task DeleteUser_WhenNotFound_ReturnsNotFound()
    {
        var usersService = new Mock<IUsersService>(MockBehavior.Strict);
        usersService.Setup(s => s.DeleteUserAsync(44)).ReturnsAsync(false);

        var controller = new UsersController(usersService.Object);

        var result = await controller.DeleteUser(44);

        Assert.IsType<NotFoundResult>(result);
        usersService.VerifyAll();
    }

    [Fact]
    public async Task DeleteUser_WhenDeleted_ReturnsNoContent()
    {
        var usersService = new Mock<IUsersService>(MockBehavior.Strict);
        usersService.Setup(s => s.DeleteUserAsync(44)).ReturnsAsync(true);

        var controller = new UsersController(usersService.Object);

        var result = await controller.DeleteUser(44);

        Assert.IsType<NoContentResult>(result);
        usersService.VerifyAll();
    }
}
