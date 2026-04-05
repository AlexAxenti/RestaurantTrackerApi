using RestaurantTracker.Api.Dtos;

namespace RestaurantTracker.Api.Services;

public interface IGooglePlacesService
{
    Task<IReadOnlyList<AutocompletePlaceDto>> AutocompleteAsync(
        string input,
        string sessionToken,
        double? latitude,
        double? longitude,
        CancellationToken cancellationToken = default);

    Task<ResolvedPlaceDto> ResolveAsync(
        string placeId,
        string sessionToken,
        CancellationToken cancellationToken = default);
}
