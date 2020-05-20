using GoldLeadsMedia.Database.Models;
using GoldLeadsMedia.PartnersApi.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.PartnersApi.Services
{
    public interface ILeadsService
    {
        Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel);
        IEnumerable<Lead> GetAllBy(string affiliateId);
    }
}
