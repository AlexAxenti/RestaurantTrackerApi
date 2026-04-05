using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantTracker.Api.Dtos;
using RestaurantTracker.Api.Services;

namespace RestaurantTracker.Api.Controllers;

[ApiController]
[Route("api/places")]
[Authorize]
public class PlacesController : ControllerBase
{
    private readonly IGooglePlacesService _googlePlacesService;

    public PlacesController(IGooglePlacesService googlePlacesService)
    {
        _googlePlacesService = googlePlacesService;
    }

    [HttpPost("autocomplete")]
    public async Task<ActionResult<AutocompletePlacesResponse>> Autocomplete(
        [FromBody] AutocompletePlacesRequest request,
        CancellationToken cancellationToken)
    {
        var input = request.Input?.Trim();
        if (string.IsNullOrEmpty(input))
            return BadRequest("Input is required.");

        if (string.IsNullOrWhiteSpace(request.SessionToken))
            return BadRequest("SessionToken is required.");

        try
        {
            var places = await _googlePlacesService.AutocompleteAsync(
                input, request.SessionToken, request.Latitude, request.Longitude, cancellationToken);

            return Ok(new AutocompletePlacesResponse(places));
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status502BadGateway,
                "Failed to reach the places provider. Please try again.");
        }
    }

    [HttpPost("resolve")]
    public async Task<ActionResult<ResolvedPlaceDto>> Resolve(
        [FromBody] ResolvePlaceRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.PlaceId))
            return BadRequest("PlaceId is required.");

        if (string.IsNullOrWhiteSpace(request.SessionToken))
            return BadRequest("SessionToken is required.");

        try
        {
            var resolved = await _googlePlacesService.ResolveAsync(
                request.PlaceId, request.SessionToken, cancellationToken);

            return Ok(resolved);
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status502BadGateway,
                "Failed to reach the places provider. Please try again.");
        }
    }
}
