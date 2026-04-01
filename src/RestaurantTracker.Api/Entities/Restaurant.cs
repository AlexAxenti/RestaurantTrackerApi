namespace RestaurantTracker.Api.Entities;

//TODO Delete after not used in search mock data anymore
public class Restaurant
{
    public int Id { get; set; }
    public string GooglePlaceId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FormattedAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastSynced { get; set; }
}