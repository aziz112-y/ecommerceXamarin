using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using e_comm.Model;

namespace e_comm.Services
{
    public class CategorieService
    {
        private readonly HttpClient _client;

        public CategorieService()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://192.168.32.175:8080/api/categories") };
        }

        public async Task<List<Categorie>> GetCategoriesAsync()
        {
            try
            {
                var response = await _client.GetStringAsync("");
                return JsonSerializer.Deserialize<List<Categorie>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching categories: {ex.Message}");
                return new List<Categorie>(); 
            }
        }
    }
}
