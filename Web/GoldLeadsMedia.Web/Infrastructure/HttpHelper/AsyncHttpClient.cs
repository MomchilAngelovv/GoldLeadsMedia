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
            var responseAsString = await response.Content.ReadAsStringAsync();

            var mappedResponse = this.MapResponse<T>(responseAsString);
            return mappedResponse;
        }

        public async Task<T> GetAsync<T>(string url, object filter = null)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append($"{this.configuration["CoreApiUrl"]}/{url}");

            if (filter != null)
            {
                urlBuilder.Append($"?");
                var filterProperties = filter.GetType().GetProperties();

                foreach (var property in filterProperties)
                {
                    var propertyValue = property.GetValue(filter);
                    if (propertyValue != null)
                    {
                        //TODO IF property is string somehow it still remains in url as empty string
                        var popertyName = property.Name;
                        urlBuilder.Append($"{popertyName}={propertyValue}&");
                    }
                }
            }

            var completeUrl = urlBuilder.ToString().TrimEnd('&','?');

            var response = await this.httpClient.GetAsync(completeUrl);
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
