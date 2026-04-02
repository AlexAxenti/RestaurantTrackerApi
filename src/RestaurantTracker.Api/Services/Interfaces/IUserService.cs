using RestaurantTracker.Api.Dtos;

namespace RestaurantTracker.Api.Services;

public interface IUserService
{
    Task<UserResponse?> GetCurrentUserAsync(string userId);
}