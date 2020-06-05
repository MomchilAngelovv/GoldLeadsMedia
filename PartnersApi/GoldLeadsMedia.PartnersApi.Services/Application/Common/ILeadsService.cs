namespace GoldLeadsMedia.PartnersApi.Services.Application.Common
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.PartnersApi.Models.ServiceModels;

    public interface ILeadsService
    {
        Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel);
        IEnumerable<Lead> GetAllBy(string affiliateId);
    }
}
