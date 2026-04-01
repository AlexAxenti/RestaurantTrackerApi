using RestaurantTracker.Api.Data;
using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public class RestaurantEntryService : IRestaurantEntryService
{
    private readonly IRestaurantService _restaurantService;
    private static readonly List<RestaurantEntry> RestaurantEntries = MockRestaurantEntries.RestaurantEntries;

    public RestaurantEntryService(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

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
        var restaurant = _restaurantService.GetById(entry.RestaurantId)!;
        return Task.FromResult<RestaurantEntryResponse?>(ToResponse(entry, restaurant));
    }

    public Task<IEnumerable<RestaurantEntryResponse>> GetRestaurantEntriesByUserIdAsync(int userId, EntryStatus? status = null)
    {
        var result = RestaurantEntries
            .Where(e => e.UserId == userId && (!status.HasValue || e.Status == status.Value))
            .Select(e => ToResponse(e, _restaurantService.GetById(e.RestaurantId)!));
        return Task.FromResult(result.AsEnumerable());
    }

    public Task<RestaurantEntryResponse> CreateRestaurantEntryAsync(CreateRestaurantEntryRequest request)
    {
        var restaurant = _restaurantService.GetByGooglePlaceId(request.GooglePlaceId);

        if (restaurant == null)
        {
            restaurant = _restaurantService.Create(new Restaurant
            {
                GooglePlaceId = request.GooglePlaceId,
                Name = request.RestaurantName,
                FormattedAddress = request.RestaurantFormattedAddress,
                City = request.RestaurantCity,
                Region = request.RestaurantRegion,
                Country = request.RestaurantCountry,
                PostalCode = request.RestaurantPostalCode
            });
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
        var restaurant = _restaurantService.GetById(entry.RestaurantId)!;
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