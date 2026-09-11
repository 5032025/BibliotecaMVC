using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PrimerSemana.Services
{
    public interface IService_API
    {
        Task<List<T>> GetAsync<T>(string endpoint);
        Task<T> GetSingleAsync<T>(string endpoint);
        Task<HttpResponseMessage> PostAsync<T>(string endpoint, T model);
        Task<HttpResponseMessage> PutAsync<T>(string endpoint, T model);
        Task<HttpResponseMessage> DeleteAsync(string endpoint);
    }

   
}