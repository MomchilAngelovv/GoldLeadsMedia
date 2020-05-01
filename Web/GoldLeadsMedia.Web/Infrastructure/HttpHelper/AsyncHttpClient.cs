namespace GoldLeadsMedia.Web.Infrastructure.HttpHelper
{
    using System.Text;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

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
            
        public async Task<T> PostAsync<T>(string url, object body, string mimeType = "application/json")
        {
            var bodyAsString = JsonSerializer.Serialize(body);
            var requestBody = new StringContent(bodyAsString, Encoding.UTF8, mimeType);

            var completeUrl = $"{this.configuration["CoreApiUrl"]}/{url}";
            var response = await this.httpClient.PostAsync(completeUrl, requestBody);
            var responseBody = await response.Content.ReadAsStringAsync();

            var mappedResponse = this.MapResponse<T>(responseBody);
            return mappedResponse;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var completeUrl = $"{this.configuration["CoreApiUrl"]}/{url}";
            var response = await this.httpClient.GetAsync(completeUrl);
            var responseBody = await response.Content.ReadAsStringAsync();
           
            var mappedResponse = this.MapResponse<T>(responseBody);
            return mappedResponse;
        }

        private T MapResponse<T>(string responseBody)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var mappedResponse = JsonSerializer.Deserialize<T>(responseBody, options);
            return mappedResponse;
        }
    }
}
