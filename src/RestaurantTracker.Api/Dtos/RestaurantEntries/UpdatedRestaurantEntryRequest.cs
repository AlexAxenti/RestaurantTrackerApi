public record UpdateRestaurantEntryRequest(
    EntryStatus? Status,
    float? Rating,
    string? Notes,
    DateTimeOffset? VisitedAt
);