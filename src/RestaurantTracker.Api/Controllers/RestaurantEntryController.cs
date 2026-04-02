using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;
using RestaurantTracker.Api.Services;

namespace RestaurantTracker.Api.Controllers;

[ApiController]
[Route("api/restaurant-entries")]
[Authorize]
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return await _restaurantEntryService.GetRestaurantEntryByIdAsync(id, userId);
    }

    [HttpGet]
    public async Task<IEnumerable<RestaurantEntryResponse>> GetAll([FromQuery] EntryStatus? status = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return await _restaurantEntryService.GetRestaurantEntriesByUserIdAsync(userId, status);
    }

    [HttpPost]
    public async Task<RestaurantEntryResponse> Create([FromBody] CreateRestaurantEntryRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return await _restaurantEntryService.CreateRestaurantEntryAsync(userId, request);
    }

    [HttpPut("{id}")]
    public async Task<RestaurantEntryResponse?> Update(int id, [FromBody] UpdateRestaurantEntryRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return await _restaurantEntryService.UpdateRestaurantEntryAsync(id, userId, request);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return await _restaurantEntryService.DeleteRestaurantEntryAsync(id, userId);
    }
}
