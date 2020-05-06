using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.AsyncHttpClient
{
    public class AsyncHttpClient : IAsyncHttpClient
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;

        public AsyncHttpClient(
            IConfiguration configuration,
            IHttpClientFactory clientFactory)
        {
            this.configuration = configuration;
            this.httpClient = clientFactory.CreateClient();
        }

        public async Task<T> GetAsync<T>(string url, object queryParameters = null)
        {
           
        }

        public async Task<T> PostAsync<T>(string url, object body, string mimeType = "application/json")
        {
            var bodyAsString = JsonSerializer.Serialize(body);
            var requestBody = new StringContent(bodyAsString, Encoding.UTF8, mimeType);

            var completeUrl = $"{this.configuration["CoreApiUrl"]}/{url}";

            var response = await this.httpClient.PostAsync(completeUrl, requestBody);
            var responseAsString = await response.Content.ReadAsStringAsync();

            var mappedResponse = this.MapResponse<T>(responseAsString);
            return mappedResponse;
        }

        private T MapResponse<T>(string responseAsString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var mappedResponse = JsonSerializer.Deserialize<T>(responseAsString, options);
            return mappedResponse;
        }
    }
}
