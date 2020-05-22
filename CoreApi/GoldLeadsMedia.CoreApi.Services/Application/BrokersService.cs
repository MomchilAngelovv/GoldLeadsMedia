using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
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
            var partners = this.db.Brokers.ToList();
            return partners;
        }

        public Broker GetBy(string id)
        {
            var partner = this.db.Brokers.FirstOrDefault(partner => partner.Id == id);
            return partner;
        }

        public async Task<Broker> RegisterAsync(PartnersRegisterInputServiceModel serviceModel)
        {
            var partner = new Broker
            {
                Name = serviceModel.Name
            };

            await this.db.Brokers.AddAsync(partner);
            await this.db.SaveChangesAsync();

            return partner;
        }
    }
}
