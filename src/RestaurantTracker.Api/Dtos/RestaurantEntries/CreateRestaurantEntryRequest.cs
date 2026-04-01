using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Dtos;

public record CreateRestaurantEntryRequest(
    int UserId,
    EntryStatus Status,
    float? Rating,
    string? Notes,
    DateTimeOffset? VisitedAt,

    // Restaurant data
    string GooglePlaceId,
    string RestaurantName,
    string RestaurantFormattedAddress,
    string RestaurantCity,
    string RestaurantRegion,
    string RestaurantCountry,
    string RestaurantPostalCode
);