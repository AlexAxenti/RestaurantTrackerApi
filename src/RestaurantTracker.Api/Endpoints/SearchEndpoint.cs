using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Endpoints;

public static class SearchEndpoint
{
    // Separate mock data simulating results from an external API (e.g. Google Places)
    private static readonly List<Restaurant> ExternalResults = new()
    {
        new Restaurant { Id = 100, GooglePlaceId = "ext_1", Name = "Popeyes", FormattedAddress = "123 Queen St W, Toronto, ON", City = "Toronto", Region = "ON", Country = "Canada", PostalCode = "M5H 2M9" },
        new Restaurant { Id = 101, GooglePlaceId = "ext_2", Name = "Popeyes", FormattedAddress = "456 Yonge St, Toronto, ON", City = "Toronto", Region = "ON", Country = "Canada", PostalCode = "M4Y 1X4" },
        new Restaurant { Id = 102, GooglePlaceId = "ext_3", Name = "Popeyes", FormattedAddress = "789 Dundas St, Mississauga, ON", City = "Mississauga", Region = "ON", Country = "Canada", PostalCode = "L5B 1T6" },
        new Restaurant { Id = 103, GooglePlaceId = "ext_4", Name = "McDonald's", FormattedAddress = "321 King St, Hamilton, ON", City = "Hamilton", Region = "ON", Country = "Canada", PostalCode = "L8P 1B5" },
        new Restaurant { Id = 104, GooglePlaceId = "ext_5", Name = "McDonald's", FormattedAddress = "654 Main St, Burlington, ON", City = "Burlington", Region = "ON", Country = "Canada", PostalCode = "L7R 3N2" },
    };

    public static void Map(WebApplication app)
    {
        app.MapGet("/api/search", (string? query, string? locationText) =>
        {
            var results = ExternalResults.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                results = results.Where(r => r.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            return Results.Ok(results);
        });
    }
}
