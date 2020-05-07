namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;
    using GoldLeadsMedia.Database.Models;

    public interface ILeadsService
    {
        Task<Lead> SendSuccessUpdateLeadAsync(Lead lead, string partnerId, string idInPartner);
        Task<LeadError> RegisterErrorAsync(LeadsRegisterErrorInputServiceModel serviceModel);
        Lead GetBy(string id);
        IEnumerable<Lead> GetAllBy(string userId);
        Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel);
    }
}
