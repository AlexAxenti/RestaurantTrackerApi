namespace RestaurantTracker.Api.Dtos;

public sealed record AutocompletePlacesRequest(
    string Input,
    string SessionToken,
    double? Latitude,
    double? Longitude);
