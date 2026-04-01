using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Dtos;

public record RestaurantEntryResponse(
    int Id,
    int UserId,
    int RestaurantId,
    EntryStatus Status,
    float? Rating,
    string? Notes,
    DateTimeOffset? VisitedAt,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    string RestaurantName,
    string RestaurantFormattedAddress,
    string RestaurantCity,
    string RestaurantRegion,
    string RestaurantCountry,
    string RestaurantPostalCode
);