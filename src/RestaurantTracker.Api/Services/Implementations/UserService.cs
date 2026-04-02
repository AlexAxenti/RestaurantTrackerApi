using RestaurantTracker.Api.Data;
using RestaurantTracker.Api.Dtos;

namespace RestaurantTracker.Api.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserResponse?> GetCurrentUserAsync(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return null;
        return new UserResponse(user.Id, user.Email);
    }
}
