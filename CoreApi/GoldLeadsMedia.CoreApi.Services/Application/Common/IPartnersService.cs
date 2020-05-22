using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    public interface IPartnersService
    {
        Broker GetBy(string id);
        IEnumerable<Broker> GetAll();
        Task<Broker> RegisterAsync(PartnersRegisterInputServiceModel serviceModel);
    }
}
