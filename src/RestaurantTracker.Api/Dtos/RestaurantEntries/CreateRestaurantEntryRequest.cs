using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Dtos;

public record CreateRestaurantEntryRequest(
    int UserId,
    int RestaurantId,
    EntryStatus Status,
    float? Rating,
    string? Notes,
    DateTimeOffset? VisitedAt
);