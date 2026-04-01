using RestaurantTracker.Api.Data;
using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public class RestaurantEntryService : IRestaurantEntryService
{
    private static readonly List<RestaurantEntry> RestaurantEntries = MockRestaurantEntries.RestaurantEntries;

    private static RestaurantEntryResponse ToResponse(RestaurantEntry entry) =>
        new RestaurantEntryResponse(
            entry.Id,
            entry.UserId,
            entry.Status,
            entry.Rating,
            entry.Notes,
            entry.VisitedAt,
            entry.CreatedAt,
            entry.UpdatedAt,
            entry.RestaurantName,
            entry.RestaurantAddress
        );

    public Task<RestaurantEntryResponse?> GetRestaurantEntryByIdAsync(int id)
    {
        var entry = RestaurantEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return Task.FromResult<RestaurantEntryResponse?>(null);
        return Task.FromResult<RestaurantEntryResponse?>(ToResponse(entry));
    }

    public Task<IEnumerable<RestaurantEntryResponse>> GetRestaurantEntriesByUserIdAsync(int userId, EntryStatus? status = null)
    {
        var result = RestaurantEntries
            .Where(e => e.UserId == userId && (!status.HasValue || e.Status == status.Value))
            .Select(ToResponse);
        return Task.FromResult(result.AsEnumerable());
    }

    public Task<RestaurantEntryResponse> CreateRestaurantEntryAsync(CreateRestaurantEntryRequest request)
    {
        var newEntry = new RestaurantEntry
        {
            Id = RestaurantEntries.Count == 0 ? 1 : RestaurantEntries.Max(e => e.Id) + 1,
            UserId = request.UserId,
            GooglePlaceId = request.GooglePlaceId,
            RestaurantName = request.RestaurantName,
            RestaurantAddress = request.RestaurantAddress,
            Status = request.Status,
            Rating = request.Rating,
            Notes = request.Notes,
            VisitedAt = request.VisitedAt,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        RestaurantEntries.Add(newEntry);
        return Task.FromResult(ToResponse(newEntry));
    }

    public Task<RestaurantEntryResponse?> UpdateRestaurantEntryAsync(int id, UpdateRestaurantEntryRequest request)
    {
        var entry = RestaurantEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return Task.FromResult<RestaurantEntryResponse?>(null);

        if (request.Status.HasValue) entry.Status = request.Status.Value;
        if (request.Rating.HasValue) entry.Rating = request.Rating.Value;
        if (request.Notes != null) entry.Notes = request.Notes;
        if (request.VisitedAt.HasValue) entry.VisitedAt = request.VisitedAt.Value;
        if (request.RestaurantName != null) entry.RestaurantName = request.RestaurantName;
        if (request.RestaurantAddress != null) entry.RestaurantAddress = request.RestaurantAddress;

        entry.UpdatedAt = DateTimeOffset.UtcNow;
        return Task.FromResult<RestaurantEntryResponse?>(ToResponse(entry));
    }

    public Task<bool> DeleteRestaurantEntryAsync(int id)
    {
        var entry = RestaurantEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return Task.FromResult(false);

        RestaurantEntries.Remove(entry);
        return Task.FromResult(true);
    }
}