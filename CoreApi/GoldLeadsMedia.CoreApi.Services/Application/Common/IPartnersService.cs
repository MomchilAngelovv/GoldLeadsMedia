using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.Database.Models;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    public interface IPartnersService
    {
        Task<Partner> RegisterAsync(PartnersRegisterServiceModel serviceModel);
    }
}
