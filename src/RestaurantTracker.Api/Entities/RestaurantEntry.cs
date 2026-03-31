public class RestaurantEntry
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public int RestaurantId { get; set; }
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