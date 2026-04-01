using RestaurantTracker.Api.Data;
using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public class UserService : IUserService
{
    private static readonly List<User> Users = MockUsers.Users;

    public Task<User?> GetUserByIdAsync(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

    public Task<User?> UpdateUserAsync(int id, UpdateUserRequest request)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return Task.FromResult<User?>(null);

        user.Name = request.Name;
        user.UpdatedAt = DateTimeOffset.UtcNow;

        return Task.FromResult<User?>(user);
    }
}
