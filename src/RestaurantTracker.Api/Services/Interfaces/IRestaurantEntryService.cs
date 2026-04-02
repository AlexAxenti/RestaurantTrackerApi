using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public interface IRestaurantEntryService
{
    Task<RestaurantEntryResponse> CreateRestaurantEntryAsync(CreateRestaurantEntryRequest request);
    Task<RestaurantEntryResponse?> GetRestaurantEntryByIdAsync(int id);
    Task<IEnumerable<RestaurantEntryResponse>> GetRestaurantEntriesByUserIdAsync(string userId, EntryStatus? status = null);
    Task<RestaurantEntryResponse?> UpdateRestaurantEntryAsync(int id, UpdateRestaurantEntryRequest request);
    Task<bool> DeleteRestaurantEntryAsync(int id);
}