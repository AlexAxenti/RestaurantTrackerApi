using RestaurantTracker.Api.Entities;

namespace RestaurantTracker.Api.Services;

public interface IRestaurantService
{
    Restaurant? GetById(int id);
    Restaurant? GetByGooglePlaceId(string googlePlaceId);
    Restaurant Create(Restaurant restaurant);
}
