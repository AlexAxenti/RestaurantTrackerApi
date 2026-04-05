namespace RestaurantTracker.Api.Dtos;

public sealed record ResolvedPlaceDto(
    string PlaceId,
    string Name,
    string FormattedAddress);
