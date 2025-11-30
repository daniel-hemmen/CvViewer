using System.Net.Http.Json;

namespace CvViewer.ComponentTests
{
    public sealed class TestApiClient
    {
        private readonly HttpClient _httpClient;

        private readonly struct Routes
        {
            public const string FavoritedCount = "/api/cvs/count/favorited";
        }

        public TestApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetFavoritedCountAsync(CancellationToken cancellationToken)
            => await _httpClient.GetFromJsonAsync<int>(Routes.FavoritedCount, cancellationToken);
    }
}
