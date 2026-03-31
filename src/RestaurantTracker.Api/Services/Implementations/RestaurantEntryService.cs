public class RestaurantEntryService : IRestaurantEntryService
{
    private static readonly List<Restaurant> Restaurants = MockRestaurants.Restaurants;
    private static readonly List<RestaurantEntry> RestaurantEntries = MockRestaurantEntries.RestaurantEntries;

    public Task<RestaurantEntry> CreateRestaurantEntryAsync(CreateRestaurantEntryRequest request)
    {
        var newEntry = new RestaurantEntry
        {
            Id = RestaurantEntries.Max(e => e.Id) + 1,
            UserId = request.UserId,
            RestaurantId = request.RestaurantId,
            Status = request.Status,
            Rating = request.Rating,
            Notes = request.Notes,
            VisitedAt = request.VisitedAt,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        RestaurantEntries.Add(newEntry);
        return Task.FromResult(newEntry);
    }

    public Task<RestaurantEntry?> GetRestaurantEntryByIdAsync(int id)
    {
        var entry = RestaurantEntries.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(entry);
    }

    public Task<IEnumerable<RestaurantEntry>> GetAllRestaurantEntriesAsync()
    {
        return Task.FromResult(RestaurantEntries.AsEnumerable());
    }

    public Task<RestaurantEntry?> UpdateRestaurantEntryAsync(int id, UpdateRestaurantEntryRequest request)
    {
        var entry = RestaurantEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return Task.FromResult<RestaurantEntry?>(null);

        if (request.Status.HasValue) entry.Status = request.Status.Value;
        if (request.Rating.HasValue) entry.Rating = request.Rating.Value;
        if (request.Notes != null) entry.Notes = request.Notes;
        if (request.VisitedAt.HasValue) entry.VisitedAt = request.VisitedAt.Value;

        entry.UpdatedAt = DateTimeOffset.UtcNow;
        return Task.FromResult<RestaurantEntry?>(entry);
    }

    public Task<bool> DeleteRestaurantEntryAsync(int id)
    {
        var entry = RestaurantEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null) return Task.FromResult(false);

        RestaurantEntries.Remove(entry);
        return Task.FromResult(true);
    }

}