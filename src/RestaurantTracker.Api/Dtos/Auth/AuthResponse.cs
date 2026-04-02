namespace RestaurantTracker.Api.Dtos;

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAtUtc
);