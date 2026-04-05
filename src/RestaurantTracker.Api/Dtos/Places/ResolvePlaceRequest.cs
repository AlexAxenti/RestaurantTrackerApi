namespace RestaurantTracker.Api.Dtos;

public sealed record ResolvePlaceRequest(
    string PlaceId,
    string SessionToken);
