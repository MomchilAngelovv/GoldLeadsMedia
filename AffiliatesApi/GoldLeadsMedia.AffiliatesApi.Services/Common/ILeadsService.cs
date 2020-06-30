namespace GoldLeadsMedia.AffiliatesApi.Services.Common
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.AffiliatesApi.Models.Services.Input;

    public interface ILeadsService
    {
        Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel);
        IEnumerable<Lead> GetAllBy(string affiliateId);
        Lead GetByEmail(string email);
    }
}
