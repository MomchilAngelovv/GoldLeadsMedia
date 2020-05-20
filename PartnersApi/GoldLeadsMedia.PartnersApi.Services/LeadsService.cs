using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using GoldLeadsMedia.PartnersApi.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.PartnersApi.Services
{
    public class LeadsService : ILeadsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public LeadsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public async Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel)
        {
            var apiRegistration = new ApiRegistration
            {
                AffiliateId = serviceModel.AffiliateId,
                OfferId = serviceModel.OfferId
            };

            await this.db.ApiRegistrations.AddAsync(apiRegistration);

            var lead = new Lead
            {
                FirstName = serviceModel.FirstName,
                LastName = serviceModel.LastName,
                Email = serviceModel.Email,
                CountryId = serviceModel.CountryId,
                PhoneNumber = serviceModel.PhoneNumber,
                ApiRegistrationId = apiRegistration.Id,
            };

            await this.db.Leads.AddAsync(lead);
            await this.db.SaveChangesAsync();

            return lead;
        }
    }
}
