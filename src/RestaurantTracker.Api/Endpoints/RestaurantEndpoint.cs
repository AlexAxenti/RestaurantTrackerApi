using RestaurantTracker.Api.Services;

namespace RestaurantTracker.Api.Endpoints;

public static class RestaurantEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/api/restaurants/{id}", (int id, IRestaurantService restaurantService) =>
        {
            var restaurant = restaurantService.GetById(id);
            return restaurant is not null ? Results.Ok(restaurant) : Results.NotFound();
        });
    }
}
