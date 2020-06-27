namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class LeadsService : ILeadsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public LeadsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public async Task<Lead> FtdSuccessAsync(Lead lead, DateTime ftdBecomeOn, string callStatus)
        {
            lead.FtdBecameOn = ftdBecomeOn;
            lead.CallStatus = callStatus;

            this.db.Leads.Update(lead);
            await this.db.SaveChangesAsync();

            return lead;
        }
        public IEnumerable<Lead> GetLeadsBy(string affiliateId)
        {
            return this.db.Leads
                .Where(lead => lead.ClickRegistration.AffiliateId == affiliateId)
                .ToList();
        }
        public Lead GetBy(string id, bool searchByBrokerId)
        {
            if (searchByBrokerId)
            {
                return this.db.Leads
                    .FirstOrDefault(lead => lead.IdInBroker == id);
            }

            return this.db.Leads
                .FirstOrDefault(lead => lead.Id == id);
        }
        public async Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel)
        {
            var lead = new Lead
            {
                FirstName = serviceModel.FirstName,
                LastName = serviceModel.LastName,
                Password = serviceModel.Password,
                Email = serviceModel.Email,
                CountryId = serviceModel.CountryId,
                PhoneNumber = serviceModel.PhoneNumber,
                ClickRegistrationId = serviceModel.ClickRegistrationId,
            };

            await this.db.Leads.AddAsync(lead);
            await this.db.SaveChangesAsync();

            return lead;
        }
        public async Task<Lead> SendLeadSuccessAsync(Lead lead, string brokerId, string idInBroker)
        {
            lead.UpdatedOn = DateTime.UtcNow;
            lead.BrokerId = brokerId;
            lead.IdInBroker = idInBroker;

            this.db.Leads.Update(lead);
            await this.db.SaveChangesAsync();

            return lead;
        }
        public IEnumerable<Lead> GetAll()
        {
            return this.db.Leads.ToList();
        }
    }
}
