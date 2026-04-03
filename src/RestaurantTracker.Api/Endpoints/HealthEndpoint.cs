namespace RestaurantTracker.Api.Endpoints;

public static class HealthEndpoint
{
	public static void Map(WebApplication app)
	{
		app.MapGet("/api/health", () =>
		{
			return Results.Ok(new
			{
				status = "healthy",
				utcTime = DateTime.UtcNow
			});
		});
	}
}
