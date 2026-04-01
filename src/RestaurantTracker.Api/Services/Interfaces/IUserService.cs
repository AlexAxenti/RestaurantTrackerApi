using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> UpdateUserAsync(int id, UpdateUserRequest request);
}