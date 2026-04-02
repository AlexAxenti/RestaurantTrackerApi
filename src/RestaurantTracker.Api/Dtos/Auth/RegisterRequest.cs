namespace RestaurantTracker.Api.Dtos;

public record RegisterRequest(
    string Email,
    string Password
);