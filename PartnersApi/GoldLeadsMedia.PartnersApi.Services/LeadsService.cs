namespace GoldLeadsMedia.PartnersApi.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.PartnersApi.Models.ServiceModels;

    public class LeadsService : ILeadsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public LeadsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Lead> GetAllBy(string affiliateId)
        {
            var leads = this.db.Leads
                .Where(lead => lead.ApiRegistration.AffiliateId == affiliateId)
                .ToList();

            return leads;
        }

        public async Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel)
        {
            var apiRegistration = new ApiRegistration
            {
                AffiliateId = serviceModel.AffiliateId,
                OfferId = serviceModel.OfferId,
                IpAddress = serviceModel.IpAddress
            };

            await this.db.ApiRegistrations.AddAsync(apiRegistration);

            var lead = new Lead
            {
                FirstName = serviceModel.FirstName,
                LastName = serviceModel.LastName,
                Password = serviceModel.Password,
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
