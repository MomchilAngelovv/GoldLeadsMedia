namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.OutputModels;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

    public interface IBrokersService
    {
        Broker GetBy(string id);
        IEnumerable<Broker> GetAll();
        Task<Broker> RegisterAsync(BrokersRegisterInputServiceModel serviceModel);
        IEnumerable<object> Summary();
    }
}
