using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace e_comm.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://192.168.32.175:8080/api/auth";

        public AuthService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var payload = new { username, password };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/register", content);

            return response.IsSuccessStatusCode;    
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var payload = new { username, password };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var role = responseBody.Contains("ADMIN") ? "ADMIN" : "USER";

                Preferences.Set("UserRole", role);

                return responseBody; 
            }

            throw new Exception("Invalid credentials");
        }
    }
}