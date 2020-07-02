namespace GoldLeadsMedia.CoreApi.Services.AsyncHttpClient
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IAsyncHttpClient
    {
        Task<string> GetAsync(string url, object queryParameters = null, Dictionary<string, string> headers = null);
        Task<T> PostAsync<T>(string url, object body, Dictionary<string, string> headers = null, string mimeType = "application/json");
        Task<T> GetAsync<T>(string url, object queryParameters = null, Dictionary<string, string> headers = null);
    }
}
