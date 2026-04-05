using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using RestaurantTracker.Api.Dtos;

namespace RestaurantTracker.Api.Services;

public class GooglePlacesService : IGooglePlacesService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly ILogger<GooglePlacesService> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public GooglePlacesService(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<GooglePlacesService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _apiKey = configuration["Google:PlacesApiKey"]
            ?? throw new InvalidOperationException("Google:PlacesApiKey is not configured.");
    }

    public async Task<IReadOnlyList<AutocompletePlaceDto>> AutocompleteAsync(
        string input,
        string sessionToken,
        double? latitude,
        double? longitude,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new GoogleAutocompleteRequest
        {
            Input = input,
            SessionToken = sessionToken,
            LocationBias = latitude.HasValue && longitude.HasValue
                ? new GoogleLocationBias
                {
                    Circle = new GoogleCircle
                    {
                        Center = new GoogleLatLng
                        {
                            Latitude = latitude.Value,
                            Longitude = longitude.Value
                        }
                    }
                }
                : null
        };

        using var request = new HttpRequestMessage(HttpMethod.Post,
            "https://places.googleapis.com/v1/places:autocomplete");

        request.Headers.Add("X-Goog-Api-Key", _apiKey);
        request.Content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            System.Text.Encoding.UTF8,
            "application/json");

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Google Autocomplete failed with status {StatusCode}", response.StatusCode);
            throw new HttpRequestException($"Google Autocomplete returned {(int)response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var result = JsonSerializer.Deserialize<GoogleAutocompleteResponse>(json, JsonOptions);

        if (result?.Suggestions is null or { Count: 0 })
            return [];

        return result.Suggestions
            .Where(s => s.PlacePrediction is not null)
            .Select(s =>
            {
                var prediction = s.PlacePrediction!;
                return new AutocompletePlaceDto(
                    PlaceId: prediction.PlaceId ?? string.Empty,
                    Name: prediction.StructuredFormat?.MainText?.Text
                        ?? prediction.Text?.Text
                        ?? string.Empty,
                    Address: prediction.StructuredFormat?.SecondaryText?.Text ?? string.Empty);
            })
            .ToList();
    }

    public async Task<ResolvedPlaceDto> ResolveAsync(
        string placeId,
        string sessionToken,
        CancellationToken cancellationToken = default)
    {
        var url = $"https://places.googleapis.com/v1/places/{Uri.EscapeDataString(placeId)}";

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-Goog-Api-Key", _apiKey);
        request.Headers.Add("X-Goog-FieldMask", "id,displayName,formattedAddress");
        request.Headers.Add("X-Goog-Session-Token", sessionToken);

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Google Place Details failed with status {StatusCode}", response.StatusCode);
            throw new HttpRequestException($"Google Place Details returned {(int)response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var result = JsonSerializer.Deserialize<GooglePlaceDetailsResponse>(json, JsonOptions);

        if (result is null)
            throw new HttpRequestException("Google Place Details returned an empty response.");

        var resolved = new ResolvedPlaceDto(
            PlaceId: result.Id ?? placeId,
            Name: result.DisplayName?.Text ?? string.Empty,
            FormattedAddress: result.FormattedAddress ?? string.Empty);

        return resolved;
    }
}
