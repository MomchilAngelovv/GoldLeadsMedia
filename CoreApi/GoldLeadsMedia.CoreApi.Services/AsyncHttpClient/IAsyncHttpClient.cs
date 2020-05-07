using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.AsyncHttpClient
{
    public interface IAsyncHttpClient
    {
        Task<T> PostAsync<T>(string url, object body, Dictionary<string, string> headers = null, string mimeType = "application/json");
        Task<T> GetAsync<T>(string url, object queryParameters = null);
    }
}
