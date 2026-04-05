namespace RestaurantTracker.Api.Dtos;

public sealed record AutocompletePlaceDto(
    string PlaceId,
    string Name,
    string Address);

public sealed record AutocompletePlacesResponse(
    IReadOnlyList<AutocompletePlaceDto> Places);
