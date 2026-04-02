namespace RestaurantTracker.Api.Dtos;

public record LoginRequest(
    string Email,
    string Password
);