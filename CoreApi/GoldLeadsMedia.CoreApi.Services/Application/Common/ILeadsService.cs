namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.Database.Models;

    public interface ILeadsService
    {
        IEnumerable<Lead> GetAllBy(string userId);
        Task<Lead> RegisterAsync(LeadsRegisterServiceModel serviceModel);
    }
}
