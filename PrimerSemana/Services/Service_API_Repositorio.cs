using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PrimerSemana.Services
{
    public class Service_API_Repositorio : IService_API
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Service_API_Repositorio(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpClient CreateClientWithAuth()
        {
            var client = _httpClientFactory.CreateClient("BookApi");

            var token = _httpContextAccessor.HttpContext?.User.FindFirst("Token")?.Value;
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            var client = CreateClientWithAuth();
            return await client.GetFromJsonAsync<List<T>>(endpoint) ?? new List<T>();
        }

        public async Task<T> GetSingleAsync<T>(string endpoint)
        {
            var client = CreateClientWithAuth();
            return await client.GetFromJsonAsync<T>(endpoint)!;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T model)
        {
            var client = CreateClientWithAuth();
            return await client.PostAsJsonAsync(endpoint, model);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T model)
        {
            var client = CreateClientWithAuth();
            return await client.PutAsJsonAsync(endpoint, model);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            var client = CreateClientWithAuth();
            return await client.DeleteAsync(endpoint);
        }
    }
}