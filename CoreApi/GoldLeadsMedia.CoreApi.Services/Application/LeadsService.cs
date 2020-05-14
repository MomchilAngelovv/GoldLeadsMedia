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
                .Where(lead => lead.Click.AffiliateId == affiliateId);

            return leads;
        }
        public Lead GetBy(string id, bool idInPartner)
        {
            if (idInPartner)
            {
                return this.db.Leads.FirstOrDefault(lead => lead.IdInPartner == id);
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
                Password = serviceModel.Password,
                CountryId = serviceModel.CountryId,
                PhoneNumber = serviceModel.PhoneNumber,
                ClickId = serviceModel.ClickId
            };

            await this.db.Leads.AddAsync(lead);
            await this.db.SaveChangesAsync();

            return lead;
        }
        public async Task<Lead> SendSuccessUpdateLeadAsync(Lead lead, string partnerId, string idInPartner)
        {
            lead.UpdatedOn = DateTime.UtcNow;
            lead.PartnerId = partnerId;
            lead.IdInPartner = idInPartner;

            this.db.Leads.Update(lead);
            await this.db.SaveChangesAsync();

            return lead;
        }
    }
}
