using RestaurantTracker.Api.Data;
using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public class RestaurantService : IRestaurantService
{
    private static readonly List<Restaurant> Restaurants = MockRestaurants.Restaurants;

    public Restaurant? GetById(int id)
    {
        return Restaurants.FirstOrDefault(r => r.Id == id);
    }

    public Restaurant? GetByGooglePlaceId(string googlePlaceId)
    {
        return Restaurants.FirstOrDefault(r => r.GooglePlaceId == googlePlaceId);
    }

    public Restaurant Create(Restaurant restaurant)
    {
        restaurant.Id = Restaurants.Count > 0 ? Restaurants.Max(r => r.Id) + 1 : 1;
        restaurant.CreatedAt = DateTimeOffset.UtcNow;
        restaurant.LastSynced = DateTimeOffset.UtcNow;
        Restaurants.Add(restaurant);
        return restaurant;
    }
}
