using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Dtos;

public record CreateRestaurantEntryRequest(
    EntryStatus Status,
    float? Rating,
    string? Notes,
    DateTimeOffset? VisitedAt,

    // Restaurant data
    string GooglePlaceId,
    string RestaurantName,
    string RestaurantAddress
);