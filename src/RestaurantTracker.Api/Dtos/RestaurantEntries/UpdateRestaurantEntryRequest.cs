using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Dtos;

public record UpdateRestaurantEntryRequest(
    EntryStatus? Status,
    float? Rating,
    string? Notes,
    DateTimeOffset? VisitedAt
);