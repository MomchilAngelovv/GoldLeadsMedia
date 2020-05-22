using GoldLeadsMedia.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    public interface ILandingPagesService
    {
        IEnumerable<LandingPage> GetAll();
        LandingPage GetBy(string id);
        Task<LandingPage> CreateAsync(string name, string url);
    }
}
