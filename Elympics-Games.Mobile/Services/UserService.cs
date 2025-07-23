using Elympics_Games.Mobile.DTOs.User;
using Elympics_Games.Mobile.Helpers;
using Elympics_Games.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Elympics_Games.Mobile.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UserService()
        {
            // Used only in development environment
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                _baseUrl = "https://10.0.2.2:7175/api/users";
            }
            else
            {
                _baseUrl = "https://localhost:7175/api/users";
            }
        }

        public async Task<ResultModel> CreateUserAsync(CreateUserDto dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_baseUrl, content);
                var responseText = await response.Content.ReadAsStringAsync();

                return new ResultModel
                {
                    Success = response.IsSuccessStatusCode,
                    Message = response.IsSuccessStatusCode
                        ? "User created successfully!"
                        : responseText,
                    StatusCode = response.StatusCode
                };
            }
            catch (HttpRequestException ex)
            {
                return new ResultModel
                {
                    Success = false,
                    Message = $"Network error: {ex.Message}",
                    StatusCode = HttpStatusCode.ServiceUnavailable
                };
            }
            catch (Exception ex)
            {
                return new ResultModel
                {
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
