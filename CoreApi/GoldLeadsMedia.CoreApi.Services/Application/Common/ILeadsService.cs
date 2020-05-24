namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;

    public interface ILeadsService
    {
        Task<Lead> SendSuccessUpdateLeadAsync(Lead lead, string brokerId, string idInBroker);
        Task<Lead> FtdBecomeUpdateLeadAsync(Lead lead, DateTime ftdBecomeOn, string callStatus);
        Lead GetBy(string id, bool searchByBrokerId = false);
        IEnumerable<Lead> GetAllBy(string userId);
        Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel);
    }
}
