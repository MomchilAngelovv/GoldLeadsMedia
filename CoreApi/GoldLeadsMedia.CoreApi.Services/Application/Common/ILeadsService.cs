namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.Database.Models;

    public interface ILeadsService
    {
        Lead GetBy(string id);
        IEnumerable<Lead> GetAllBy(string userId);
        Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel);
    }
}
