﻿namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;
    using GoldLeadsMedia.Database.Models;

    public interface ILeadsService
    {
        Task<Lead> SendSuccessUpdateLeadAsync(Lead lead, string brokerId, string idInBroker);
        Task<Lead> FtdBecomeUpdateLeadAsync(Lead lead, DateTime ftdBecomeOn, string callStatus);
        Lead GetBy(string id, bool searchByBrokerId = false);
        IEnumerable<Lead> GetAllBy(string userId);
        Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel);
    }
}
