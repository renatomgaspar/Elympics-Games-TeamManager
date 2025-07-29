using Elympics_Games.Mobile.Helpers;
using Elympics_Games.Mobile.Models;
using System.Net;
using System.Text;
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

        public async Task<bool> AddTeamAsync(Team team)
        {
            try
            {
                var json = JsonSerializer.Serialize(team);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}", content);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ Network error (AddTeam): {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected error (AddTeam): {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateTeamAsync(Team team)
        {
            try
            {
                var json = JsonSerializer.Serialize(team);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var requestUrl = $"{_baseUrl}/{team.Id}";

                var response = await _httpClient.PutAsync(requestUrl, content);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ Network error (UpdateTeam): {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected error (UpdateTeam): {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            try
            {
                var requestUrl = $"{_baseUrl}/{teamId}";

                var response = await _httpClient.DeleteAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ Network error (DeleteTeam): {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected error (DeleteTeam): {ex.Message}");
                return false;
            }
        }
    }
}
