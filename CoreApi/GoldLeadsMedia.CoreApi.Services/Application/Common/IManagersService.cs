using GoldLeadsMedia.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    public interface IManagersService
    {
        Task<IEnumerable<GoldLeadsMediaUser>> GetAffiliatesByAsync(string managerId);
    }
}
