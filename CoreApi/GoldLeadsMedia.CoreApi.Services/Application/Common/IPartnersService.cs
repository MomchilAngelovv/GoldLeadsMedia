using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    public interface IPartnersService
    {
        Partner GetBy(string id);
        IEnumerable<Partner> GetAll();
        Task<Partner> RegisterAsync(PartnersRegisterInputServiceModel serviceModel);
    }
}
