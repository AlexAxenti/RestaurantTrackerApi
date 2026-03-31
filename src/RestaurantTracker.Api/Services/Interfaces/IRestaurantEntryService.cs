using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public interface IRestaurantEntryService
{
    Task<RestaurantEntry> CreateRestaurantEntryAsync(CreateRestaurantEntryRequest request);
    Task<RestaurantEntry?> GetRestaurantEntryByIdAsync(int id);
    Task<IEnumerable<RestaurantEntry>> GetAllRestaurantEntriesAsync();
    Task<RestaurantEntry?> UpdateRestaurantEntryAsync(int id, UpdateRestaurantEntryRequest request);
    Task<bool> DeleteRestaurantEntryAsync(int id);
}