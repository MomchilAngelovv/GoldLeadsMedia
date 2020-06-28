namespace GoldLeadsMedia.CoreApi.Services.Common
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;

    public interface ILandingPagesService
    {
        IEnumerable<LandingPage> GetAll();
        LandingPage GetBy(string id);
        Task<LandingPage> CreateAsync(string name, string url);
    }
}
