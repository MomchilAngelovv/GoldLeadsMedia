namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class BrokersService : IBrokersService
    {
        private readonly GoldLeadsMediaDbContext db;

        public BrokersService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Broker> GetAll()
        {
            return this.db.Brokers
                .ToList();
        }

        public Broker GetBy(string id)
        {
            return this.db.Brokers
                .FirstOrDefault(broker => broker.Id == id);
        }

        public async Task<Broker> RegisterAsync(BrokersRegisterInputServiceModel serviceModel)
        {
            var broker = new Broker
            {
                Name = serviceModel.Name
            };

            await this.db.Brokers.AddAsync(broker);
            await this.db.SaveChangesAsync();

            return broker;
        }
    }
}
