namespace GoldLeadsMedia.AffiliatesApi.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.AffiliatesApi.Services.Common;
    using GoldLeadsMedia.AffiliatesApi.Models.Services.Input;

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
            return db.Leads
                .Where(lead => lead.ApiRegistrationId != null && lead.ApiRegistration.AffiliateId == affiliateId)
                .ToList();
        }
        public async Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel)
        {
            var apiRegistration = new ApiRegistration
            {
                AffiliateId = serviceModel.AffiliateId,
                OfferId = serviceModel.OfferId,
                IpAddress = serviceModel.IpAddress
            };

            await db.ApiRegistrations.AddAsync(apiRegistration);

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

            await db.Leads.AddAsync(lead);
            await db.SaveChangesAsync();

            return lead;
        }
    }
}
