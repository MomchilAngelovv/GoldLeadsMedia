namespace GoldLeadsMedia.CoreApi.Services.Common
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

    public interface IBrokersService
    {
        Broker GetBy(string id);
        Broker GetByName(string name);
        IEnumerable<Broker> GetAll();
        Task<Broker> RegisterAsync(BrokersRegisterInputServiceModel serviceModel);
        IEnumerable<object> Summary();
    }
}
