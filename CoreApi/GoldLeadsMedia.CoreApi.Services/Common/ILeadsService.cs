namespace GoldLeadsMedia.CoreApi.Services.Common
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

    public interface ILeadsService
    {
        IEnumerable<Lead> GetAll();
        Task<Lead> SendLeadSuccessAsync(Lead lead, string brokerId, string idInBroker);
        Task<Lead> FtdSuccessAsync(Lead lead, DateTime ftdBecomeOn, string callStatus);
        Lead GetBy(string id, bool searchByBrokerId = false);
        IEnumerable<Lead> GetLeadsBy(string userId);
        Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel);
        Lead GetByEmail(string email);
    }
}
