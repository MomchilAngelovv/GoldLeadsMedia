﻿namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;
    using GoldLeadsMedia.CoreApi.Services.Common;
    using GoldLeadsMedia.CoreApi.Models.Services.Output;

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

        public Broker GetByName(string name)
        {
            return this.db.Brokers
                .FirstOrDefault(broker => broker.Name == name);
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

        public IEnumerable<object> Summary()
        {
            var brokersSummary = this.db.Brokers
                .Select(broker => new BrokersSummaryBroker
                {
                    Id = broker.Id,
                    Name = broker.Name,
                    TotalLeads = broker.Leads.Count(),
                    TotalFtds = broker.Leads.Where(lead => lead.FtdBecameOn.HasValue).Count()
                });

            return brokersSummary;
        }
    }
}
