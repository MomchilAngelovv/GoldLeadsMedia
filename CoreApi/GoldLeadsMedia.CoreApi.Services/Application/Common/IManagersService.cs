namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;

    public interface IManagersService
    {
        Task<IEnumerable<GoldLeadsMediaUser>> GetAllAffiliates();
        Task<IEnumerable<GoldLeadsMediaUser>> GetAffiliatesByAsync(string managerId);
        IEnumerable<Lead> GetNotConfirmedLeads();
        IEnumerable<Lead> GetConfirmedLeads();
        Task<IEnumerable<Lead>> ConfirmLeadsAsync(ManagersConfirmLeadsInputServiceModel serviceModel);
        GoldLeadsMediaUser GetAffiliateDetailsBy(string id);
    }
}
