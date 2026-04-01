using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Data;

public static class MockRestaurantEntries
{
    public static readonly List<RestaurantEntry> RestaurantEntries = new List<RestaurantEntry>
    {
        new RestaurantEntry
        {
            Id = 1,
            UserId = 1,
            GooglePlaceId = "1",
            RestaurantName = "Sushi Place",
            RestaurantAddress = "456 Sushi St, Sushi City",
            Status = EntryStatus.Visited,
            Rating = 4.5f,
            Notes = "Great vibe, decent food",
            VisitedAt = DateTimeOffset.UtcNow.AddDays(-3),
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-3),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-3)
        },
        new RestaurantEntry
        {
            Id = 2,
            UserId = 1,
            GooglePlaceId = "2",
            RestaurantName = "Burger Place",
            RestaurantAddress = "123 Burger St, Burger City",
            Status = EntryStatus.Planned,
            Rating = null,
            Notes = "Heard it's really good burgers",
            VisitedAt = null,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-2),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-2)
        },
        new RestaurantEntry
        {
            Id = 3,
            UserId = 2,
            GooglePlaceId = "1",
            RestaurantName = "Jack's Diner",
            RestaurantAddress = "789 Diner St, Diner City",
            Status = EntryStatus.Planned,
            Rating = null,
            Notes = null,
            VisitedAt = null,
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-1),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-1)
        },
        new RestaurantEntry
        {
            Id = 4,
            UserId = 2,
            GooglePlaceId = "3",
            RestaurantName = "Sushi Place",
            RestaurantAddress = "456 Sushi St, Sushi City",
            Status = EntryStatus.Visited,
            Rating = 5.0f,
            Notes = "Amazing sushi, pricey but worth it",
            VisitedAt = DateTimeOffset.UtcNow.AddDays(-7),
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-7),
            UpdatedAt = DateTimeOffset.UtcNow.AddDays(-6)
        }
    };
}

