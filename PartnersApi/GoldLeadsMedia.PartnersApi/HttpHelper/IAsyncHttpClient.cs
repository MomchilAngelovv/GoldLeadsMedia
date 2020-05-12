using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.PartnersApi.HttpHelper
{
    public interface IAsyncHttpClient
    {
        Task<T> PostAsync<T>(string url, object body, Dictionary<string, string> headers = null, string mimeType = "application/json");
        Task<T> GetAsync<T>(string url, object queryParameters = null);
    }
}
