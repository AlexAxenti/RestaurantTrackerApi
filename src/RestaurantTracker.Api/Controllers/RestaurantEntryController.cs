using Microsoft.AspNetCore.Mvc;
using RestaurantTracker.Api.Entities;
using RestaurantTracker.Api.Services;

namespace RestaurantTracker.Api.Controllers;

[ApiController]
[Route("api/restaurant-entries")]
public class RestaurantEntryController : ControllerBase
{
    private readonly IRestaurantEntryService _restaurantEntryService;

    public RestaurantEntryController(IRestaurantEntryService restaurantEntryService)
    {
        _restaurantEntryService = restaurantEntryService;
    }

    [HttpGet]
    public IEnumerable<RestaurantEntry> Get()
    {
        return _restaurantEntryService.GetAllRestaurantEntriesAsync().Result;
    }
}
