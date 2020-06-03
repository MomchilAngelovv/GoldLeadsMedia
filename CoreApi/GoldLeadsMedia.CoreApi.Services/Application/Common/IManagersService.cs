using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    public interface IManagersService
    {
        Task<IEnumerable<GoldLeadsMediaUser>> GetAll();
        Task<IEnumerable<GoldLeadsMediaUser>> GetAffiliatesByAsync(string managerId);
        IEnumerable<Lead> GetNotConfirmedLeads();
        IEnumerable<Lead> GetConfirmedLeads();
        Task<IEnumerable<Lead>> ConfirmLeadsAsync(ManagersConfirmLeadsInputServiceModel serviceModel);
        GoldLeadsMediaUser GetAffiliateDetailsBy(string id);
    }
}
