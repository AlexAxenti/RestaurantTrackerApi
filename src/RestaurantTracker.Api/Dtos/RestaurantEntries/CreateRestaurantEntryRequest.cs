public record CreateRestaurantEntryRequest(
    int UserId,
    int RestaurantId,
    EntryStatus Status,
    float? Rating,
    string? Notes,
    DateTimeOffset? VisitedAt
);