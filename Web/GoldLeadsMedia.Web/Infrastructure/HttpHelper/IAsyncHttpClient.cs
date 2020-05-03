namespace GoldLeadsMedia.Web.Infrastructure.HttpHelper
{
    using System.Threading.Tasks;

    public interface IAsyncHttpClient
    {
        Task<T> PostAsync<T>(string url, object body, string mimeType = "application/json");
        Task<T> GetAsync<T>(string url, object filter = null);
    }
}
