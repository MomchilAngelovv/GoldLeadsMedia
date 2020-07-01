namespace GoldLeadsMedia.Web.Infrastructure.HttpHelper
{
    using System.Text;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.Extensions.Configuration;

    using GoldLeadsMedia.Web.Infrastructure.Exceptions;

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
                if (url.EndsWith("?") == false)
                {
                    urlBuilder.Append($"?");
                }

                var queryProperties = queryParameters.GetType().GetProperties();

                foreach (var property in queryProperties)
                {
                    var propertyValue = property.GetValue(queryParameters);
                    if (propertyValue != null)
                    {
                        //TODO IF property is string somehow it still remains in url as empty string
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
        public async Task<T> PostAsync<T>(string url, object body, Dictionary<string, string> headers, string mimeType = "application/json")
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

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    this.httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var completeUrl = urlBuilder.ToString();

            //TODO: think better logic for mime types
            if (mimeType == "application/json")
            {
                var bodyAsString = JsonSerializer.Serialize(body);
                var requestBody = new StringContent(bodyAsString, Encoding.UTF8, mimeType);

                var response = await this.httpClient.PostAsync(completeUrl, requestBody);
                var responseAsString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode == false)
                {
                    throw new HttpFailRequestException(responseAsString);
                }

                var mappedResponse = this.MapResponse<T>(responseAsString);
                return mappedResponse;
            }
            else
            {
                var urlEncodedParameters = new Dictionary<string, string>();

                var bodyTypeProperties = body.GetType().GetProperties();

                foreach (var bodyTypeProperty in bodyTypeProperties)
                {
                    var key = bodyTypeProperty.Name;
                    var value = bodyTypeProperty.GetValue(body).ToString();

                    urlEncodedParameters.Add(key, value);
                }

                var requestBody = new FormUrlEncodedContent(urlEncodedParameters);

                var response = await this.httpClient.PostAsync(completeUrl, requestBody);
                var responseAsString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode == false)
                {
                    throw new HttpFailRequestException(responseAsString);
                }

                var mappedResponse = this.MapResponse<T>(responseAsString);
                return mappedResponse;
            }
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
