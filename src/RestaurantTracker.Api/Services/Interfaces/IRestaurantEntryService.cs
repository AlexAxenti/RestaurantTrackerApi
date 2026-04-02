using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public interface IRestaurantEntryService
{
    Task<RestaurantEntryResponse> CreateRestaurantEntryAsync(string userId, CreateRestaurantEntryRequest request);
    Task<RestaurantEntryResponse?> GetRestaurantEntryByIdAsync(int id, string userId);
    Task<IEnumerable<RestaurantEntryResponse>> GetRestaurantEntriesByUserIdAsync(string userId, EntryStatus? status = null);
    Task<RestaurantEntryResponse?> UpdateRestaurantEntryAsync(int id, string userId, UpdateRestaurantEntryRequest request);
    Task<bool> DeleteRestaurantEntryAsync(int id, string userId);
}