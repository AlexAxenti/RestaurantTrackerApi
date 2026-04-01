using Microsoft.AspNetCore.Mvc;
using RestaurantTracker.Api.Dtos;
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

    [HttpGet("{id}")]
    public async Task<RestaurantEntry?> GetById(int id)
    {
        return await _restaurantEntryService.GetRestaurantEntryByIdAsync(id);
    }

    [HttpGet("user/{userId}")]
    public async Task<IEnumerable<RestaurantEntry>> GetByUserId(int userId)
    {
        return await _restaurantEntryService.GetRestaurantEntriesByUserIdAsync(userId);
    }

    [HttpPost]
    public async Task<RestaurantEntry> Create([FromBody] CreateRestaurantEntryRequest request)
    {
        return await _restaurantEntryService.CreateRestaurantEntryAsync(request);
    }

    [HttpPut("{id}")]
    public async Task<RestaurantEntry?> Update(int id, [FromBody] UpdateRestaurantEntryRequest request)
    {
        return await _restaurantEntryService.UpdateRestaurantEntryAsync(id, request);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _restaurantEntryService.DeleteRestaurantEntryAsync(id);
    }
}
