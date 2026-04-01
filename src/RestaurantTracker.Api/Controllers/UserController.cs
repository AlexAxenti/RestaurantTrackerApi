using Microsoft.AspNetCore.Mvc;
using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Entities;
using RestaurantTracker.Api.Services;

namespace RestaurantTracker.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<User?> GetById(int id)
    {
        return await _userService.GetUserByIdAsync(id);
    }

    [HttpPut("{id}")]
    public async Task<User?> Update(int id, [FromBody] UpdateUserRequest request)
    {
        return await _userService.UpdateUserAsync(id, request);
    }
}