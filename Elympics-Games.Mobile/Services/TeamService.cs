using Elympics_Games.Mobile.Helpers;
using Elympics_Games.Mobile.Models;
using System.Net;
using System.Text.Json;

namespace Elympics_Games.Mobile.Services
{
    public class TeamService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public TeamService()
        {
            // Used only in development environment
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                _baseUrl = "https://10.0.2.2:7175/api/teams";
            }
            else
            {
                _baseUrl = "https://localhost:7175/api/teams";
            }
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            try
            {
                var requestUrl = $"{_baseUrl}";

                var response = await _httpClient.GetAsync(requestUrl);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var teams = JsonSerializer.Deserialize<List<Team>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return teams ?? new List<Team>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ Network error: {ex.Message}");
                return new List<Team>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected error: {ex.Message}");
                return new List<Team>();
            }
        }
    }
}
