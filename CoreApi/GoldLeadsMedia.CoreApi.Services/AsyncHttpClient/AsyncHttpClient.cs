namespace GoldLeadsMedia.CoreApi.Services.AsyncHttpClient
{
    using System.Text;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;

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

        public async Task<T> GetAsync<T>(string url, object queryParameters = null, Dictionary<string, string> headers = null)
        {
            var urlBuilder = new StringBuilder();

            if (url.Contains("http") == false)
            {
                urlBuilder.Append($"{this.configuration["CoreApiUrl"]}/{url}");
            }
            else
            {
                urlBuilder.Append(url);
            }

            if (queryParameters != null)
            {
                urlBuilder.Append($"?");
                var filterProperties = queryParameters.GetType().GetProperties();

                foreach (var property in filterProperties)
                {
                    var propertyValue = property.GetValue(queryParameters);
                    if (propertyValue != null)
                    {
                        //TODO IF property is string somehow it still remains in url as empty string -> the resulting url with empty string property in query parameter is like : '.../testendpoint?name=' (it adds name to url WHILE the value is null. In my opinion it is bug)
                        var popertyName = property.Name;
                        urlBuilder.Append($"{popertyName}={propertyValue}&");
                    }
                }
            }

            var completeUrl = urlBuilder.ToString().TrimEnd('&', '?');

            var response = await this.httpClient.GetAsync(completeUrl);
            var responseAsString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode == false)
            {
                throw new HttpFailRequestException(responseAsString);
            }

            var mappedResponse = this.MapResponse<T>(responseAsString);
            return mappedResponse;
        }
        public async Task<T> PostAsync<T>(string url, object body, Dictionary<string, string> headers = null, string mimeType = "application/json")
        {
            var urlBuilder = new StringBuilder();

            if (url.Contains("http") == false)
            {
                urlBuilder.Append($"{this.configuration["CoreApiUrl"]}/{url}");
            }
            else
            {
                urlBuilder.Append(url);
            }

            var bodyAsString = JsonSerializer.Serialize(body);
            var requestBody = new StringContent(bodyAsString, Encoding.UTF8, mimeType);

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    this.httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var completeUrl = urlBuilder.ToString();

            var response = await this.httpClient.PostAsync(completeUrl, requestBody);
            var responseAsString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode == false)
            {
                throw new HttpFailRequestException(responseAsString);
            }

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
