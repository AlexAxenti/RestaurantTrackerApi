namespace RestaurantTracker.Api.Endpoints;

public static class HealthEndpoint
{
	public static void Map(WebApplication app)
	{
		app.MapGet("/api/health", async (ApplicationDbContext db) =>
		{
			await db.Database.ExecuteSqlRawAsync("SELECT 1");

			return Results.Ok(new
			{
				status = "healthy",
				utcTime = DateTime.UtcNow
			});
		});
	}
}
