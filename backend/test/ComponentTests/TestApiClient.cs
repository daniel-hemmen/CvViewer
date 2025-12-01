using System.Net.Http.Json;

namespace CvViewer.ComponentTests;

public sealed class TestApiClient
{
    private readonly HttpClient _httpClient;

    private readonly struct Endpoints
    {
        public const string FavoritedCount = "/api/cvs/count/favorited";
        public const string ToggleIsFavorited = "/api/cvs/togglefavorited/";
    }

    public TestApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetFavoritedCountAsync(CancellationToken cancellationToken)
        => await _httpClient.GetFromJsonAsync<int>(Endpoints.FavoritedCount, cancellationToken);

    public async Task<bool> ToggleCvIsFavoritedAsync(Guid cvExternalId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PutAsync($"{Endpoints.ToggleIsFavorited}{cvExternalId}", null, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<bool>(cancellationToken: cancellationToken);
    }
}
