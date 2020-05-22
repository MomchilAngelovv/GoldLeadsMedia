namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.InputModels;

    public class LeadsService : ILeadsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public LeadsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public async Task<Lead> FtdBecomeUpdateLeadAsync(Lead lead, DateTime ftdBecomeOn, string callStatus)
        {
            lead.FtdBecameOn = ftdBecomeOn;
            lead.CallStatus = callStatus;

            this.db.Leads.Update(lead);
            await this.db.SaveChangesAsync();

            return lead;
        }

        public IEnumerable<Lead> GetAllBy(string affiliateId)
        {
            var leads = db.Leads
                .Where(lead => lead.ClickRegistration.AffiliateId == affiliateId);

            return leads;
        }
        public Lead GetBy(string id, bool searchByBrokerId)
        {
            if (searchByBrokerId)
            {
                return this.db.Leads.FirstOrDefault(lead => lead.IdInBroker == id);
            }

            return this.db.Leads.FirstOrDefault(lead => lead.Id == id);
        }
        public async Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel)
        {
            var lead = new Lead
            {
                FirstName = serviceModel.FirstName,
                LastName = serviceModel.LastName,
                Email = serviceModel.Email,
                CountryId = serviceModel.CountryId,
                PhoneNumber = serviceModel.PhoneNumber,
                ClickRegistrationId = serviceModel.ClickRegistrationId,
            };

            await this.db.Leads.AddAsync(lead);
            await this.db.SaveChangesAsync();

            return lead;
        }
        public async Task<Lead> SendSuccessUpdateLeadAsync(Lead lead, string brokerId, string idInBroker)
        {
            lead.UpdatedOn = DateTime.UtcNow;
            lead.BrokerId = brokerId;
            lead.IdInBroker = idInBroker;

            this.db.Leads.Update(lead);
            await this.db.SaveChangesAsync();

            return lead;
        }
    }
}
