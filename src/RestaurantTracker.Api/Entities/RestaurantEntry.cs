namespace RestaurantTracker.Api.Entities;

public class RestaurantEntry
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty; 
    public ApplicationUser User { get; set; } = null!;
    public string? GooglePlaceId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
    public string RestaurantAddress { get; set; } = string.Empty;
    public EntryStatus Status { get; set; }
    public float? Rating { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset? VisitedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public enum EntryStatus
{
    Visited,
    Planned
}