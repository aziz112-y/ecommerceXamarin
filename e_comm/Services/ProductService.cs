using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace e_comm.Services
{
    class ProductService
    {
        private readonly string _baseUrl = "http://192.168.32.175:8080/api/products";
        private readonly HttpClient _httpClient;

        public ProductService()
        {
            _httpClient = new HttpClient();
        }

        // Get list of products
        public async Task<List<Product>> GetProduitsAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_baseUrl);

                System.Diagnostics.Debug.WriteLine("API Response: " + response);
                var products = JsonConvert.DeserializeObject<List<Product>>(response);

                foreach (var product in products)
                {
                    System.Diagnostics.Debug.WriteLine($"Product: {product.Name}, Price: {product.Price}, ProductID: {product.Id}");
                }

                return products;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task AddProductAsync(Product product)
        {
            var json = JsonConvert.SerializeObject(new
            {
                name = product.Name,
                description = product.Description,
                price = product.Price,
                categoryId = product.CategoryId 
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(_baseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine("Product added successfully.");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to add product. Status Code: {response.StatusCode}");
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("Error Response: " + errorResponse);
                    throw new Exception("Failed to add product");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Product: {product.Name}, Price: {product.Price}, ProductID: {product.Id}, Category:{product.CategoryId}");

                throw new Exception("Failed to add product");
            }
        }




        public async Task<Product> GetProductByIdAsync(String productId)
        {


            var response = await _httpClient.GetAsync($"api/products/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var productJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(productJson); 
            }

            return null;
        }
        public async Task<bool> DeleteProductAsync(string productId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{productId}");
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                throw new Exception("Failed to delete product");
                return false;
            }
        }
        
    }
}
