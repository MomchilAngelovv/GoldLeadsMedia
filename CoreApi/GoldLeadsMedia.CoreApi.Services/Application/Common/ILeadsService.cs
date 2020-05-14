namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;
    using GoldLeadsMedia.Database.Models;

    public interface ILeadsService
    {
        Task<Lead> SendSuccessUpdateLeadAsync(Lead lead, string partnerId, string idInPartner);
        Task<Lead> FtdBecomeUpdateLeadAsync(Lead lead, DateTime ftdBecomeOn, string callStatus);
        Task<LeadError> RegisterErrorAsync(LeadsRegisterErrorInputServiceModel serviceModel);
        Lead GetBy(string id, bool idInPartner = false);
        IEnumerable<Lead> GetAllBy(string userId);
        Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel);
    }
}
