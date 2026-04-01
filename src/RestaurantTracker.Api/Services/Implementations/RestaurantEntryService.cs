using RestaurantTracker.Api.Data;
using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public class RestaurantEntryService : IRestaurantEntryService
{
    private static readonly List<Restaurant> Restaurants = MockRestaurants.Restaurants;
    private static readonly List<RestaurantEntry> RestaurantEntries = MockRestaurantEntries.RestaurantEntries;

    private static RestaurantEntryResponse ToResponse(RestaurantEntry entry, Restaurant restaurant) =>
        new RestaurantEntryResponse(
            entry.Id,
            entry.UserId,
            entry.RestaurantId,
            entry.Status,
            entry.Rating,
            entry.Notes,
            entry.VisitedAt,
            entry.CreatedAt,
            entry.UpdatedAt,
            restaurant.Name,
            restaurant.FormattedAddress,
            restaurant.City,
            restaurant.Region,
            restaurant.Country,
            restaurant.PostalCode
        );

    public Task<RestaurantEntryResponse?> GetRestaurantEntryByIdAsync(int id)
    {
        var entry = RestaurantEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return Task.FromResult<RestaurantEntryResponse?>(null);
        var restaurant = Restaurants.First(r => r.Id == entry.RestaurantId);
        return Task.FromResult<RestaurantEntryResponse?>(ToResponse(entry, restaurant));
    }

    public Task<IEnumerable<RestaurantEntryResponse>> GetRestaurantEntriesByUserIdAsync(int userId, EntryStatus? status = null)
    {
        var result = RestaurantEntries
            .Where(e => e.UserId == userId && (!status.HasValue || e.Status == status.Value))
            .Select(e => ToResponse(e, Restaurants.First(r => r.Id == e.RestaurantId)));
        return Task.FromResult(result.AsEnumerable());
    }

    public Task<RestaurantEntryResponse> CreateRestaurantEntryAsync(CreateRestaurantEntryRequest request)
    {
        var restaurant = Restaurants.FirstOrDefault(r => r.GooglePlaceId == request.GooglePlaceId);

        if (restaurant == null)
        {
            restaurant = new Restaurant
            {
                Id = Restaurants.Max(r => r.Id) + 1,
                GooglePlaceId = request.GooglePlaceId,
                Name = request.RestaurantName,
                FormattedAddress = request.RestaurantFormattedAddress,
                City = request.RestaurantCity,
                Region = request.RestaurantRegion,
                Country = request.RestaurantCountry,
                PostalCode = request.RestaurantPostalCode,
                CreatedAt = DateTimeOffset.UtcNow,
                LastSynced = DateTimeOffset.UtcNow
            };
            Restaurants.Add(restaurant);
        }

        var newEntry = new RestaurantEntry
        {
            Id = RestaurantEntries.Max(e => e.Id) + 1,
            UserId = request.UserId,
            RestaurantId = restaurant.Id,
            Status = request.Status,
            Rating = request.Rating,
            Notes = request.Notes,
            VisitedAt = request.VisitedAt,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        RestaurantEntries.Add(newEntry);
        return Task.FromResult(ToResponse(newEntry, restaurant));
    }

    public Task<RestaurantEntryResponse?> UpdateRestaurantEntryAsync(int id, UpdateRestaurantEntryRequest request)
    {
        var entry = RestaurantEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return Task.FromResult<RestaurantEntryResponse?>(null);

        if (request.Status.HasValue) entry.Status = request.Status.Value;
        if (request.Rating.HasValue) entry.Rating = request.Rating.Value;
        if (request.Notes != null) entry.Notes = request.Notes;
        if (request.VisitedAt.HasValue) entry.VisitedAt = request.VisitedAt.Value;

        entry.UpdatedAt = DateTimeOffset.UtcNow;
        var restaurant = Restaurants.First(r => r.Id == entry.RestaurantId);
        return Task.FromResult<RestaurantEntryResponse?>(ToResponse(entry, restaurant));
    }

    public Task<bool> DeleteRestaurantEntryAsync(int id)
    {
        var entry = RestaurantEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return Task.FromResult(false);

        RestaurantEntries.Remove(entry);
        return Task.FromResult(true);
    }
}