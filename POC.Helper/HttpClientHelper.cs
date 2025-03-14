using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace POC.Helper
{
    public class HttpClientHelper
    {
        private readonly HttpClient _httpClient;

        public HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET Request
        public async Task<T?> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            return await HandleResponse<T>(response);
        }

        // POST Request
        public async Task<T?> PostAsync<T>(string url, object data)
        {
            var content = SerializeContent(data);
            var response = await _httpClient.PostAsync(url, content);
            return await HandleResponse<T>(response);
        }

        // PUT Request
        public async Task<T?> PutAsync<T>(string url, object data)
        {
            var content = SerializeContent(data);
            var response = await _httpClient.PutAsync(url, content);
            return await HandleResponse<T>(response);
        }

        // DELETE Request
        public async Task<bool> DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }

        // Handle API response
        private async Task<T?> HandleResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) return default;

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // Serialize Object to JSON
        private StringContent SerializeContent(object data)
        {
            var json = JsonSerializer.Serialize(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
