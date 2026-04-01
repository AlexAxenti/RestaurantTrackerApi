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
    public async Task<RestaurantEntryResponse?> GetById(int id)
    {
        return await _restaurantEntryService.GetRestaurantEntryByIdAsync(id);
    }

    [HttpGet("user/{userId}")]
    public async Task<IEnumerable<RestaurantEntryResponse>> GetByUserId(int userId, [FromQuery] EntryStatus? status = null)
    {
        return await _restaurantEntryService.GetRestaurantEntriesByUserIdAsync(userId, status);
    }

    [HttpPost]
    public async Task<RestaurantEntryResponse> Create([FromBody] CreateRestaurantEntryRequest request)
    {
        return await _restaurantEntryService.CreateRestaurantEntryAsync(request);
    }

    [HttpPut("{id}")]
    public async Task<RestaurantEntryResponse?> Update(int id, [FromBody] UpdateRestaurantEntryRequest request)
    {
        return await _restaurantEntryService.UpdateRestaurantEntryAsync(id, request);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _restaurantEntryService.DeleteRestaurantEntryAsync(id);
    }
}
