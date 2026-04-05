using System.Text.Json.Serialization;

namespace RestaurantTracker.Api.Dtos;

// --- Autocomplete ---

internal sealed class GoogleAutocompleteRequest
{
    [JsonPropertyName("input")]
    public required string Input { get; init; }

    [JsonPropertyName("sessionToken")]
    public required string SessionToken { get; init; }

    [JsonPropertyName("locationBias")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public GoogleLocationBias? LocationBias { get; init; }
}

internal sealed class GoogleLocationBias
{
    [JsonPropertyName("circle")]
    public required GoogleCircle Circle { get; init; }
}

internal sealed class GoogleCircle
{
    [JsonPropertyName("center")]
    public required GoogleLatLng Center { get; init; }

    [JsonPropertyName("radius")]
    public double Radius { get; init; } = 50000.0; // 50 km default
}

internal sealed class GoogleLatLng
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }
}

internal sealed class GoogleAutocompleteResponse
{
    [JsonPropertyName("suggestions")]
    public List<GoogleSuggestion>? Suggestions { get; init; }
}

internal sealed class GoogleSuggestion
{
    [JsonPropertyName("placePrediction")]
    public GooglePlacePrediction? PlacePrediction { get; init; }
}

internal sealed class GooglePlacePrediction
{
    [JsonPropertyName("placeId")]
    public string? PlaceId { get; init; }

    [JsonPropertyName("text")]
    public GoogleLocalizedText? Text { get; init; }

    [JsonPropertyName("structuredFormat")]
    public GoogleStructuredFormat? StructuredFormat { get; init; }
}

internal sealed class GoogleStructuredFormat
{
    [JsonPropertyName("mainText")]
    public GoogleLocalizedText? MainText { get; init; }

    [JsonPropertyName("secondaryText")]
    public GoogleLocalizedText? SecondaryText { get; init; }
}

internal sealed class GoogleLocalizedText
{
    [JsonPropertyName("text")]
    public string? Text { get; init; }
}

// --- Place Details ---

internal sealed class GooglePlaceDetailsResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("displayName")]
    public GoogleLocalizedText? DisplayName { get; init; }

    [JsonPropertyName("formattedAddress")]
    public string? FormattedAddress { get; init; }
}
