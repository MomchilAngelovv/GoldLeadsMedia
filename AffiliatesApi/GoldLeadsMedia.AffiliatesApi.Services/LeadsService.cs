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

        public Lead GetByEmail(string email)
        {
            return this.db.Leads
                .SingleOrDefault(lead => lead.Email == email);
        }

        public async Task<Lead> RegisterAsync(LeadsRegisterInputServiceModel serviceModel)
        {
            var apiRegistration = new ApiRegistration
            {
                AffiliateId = serviceModel.AffiliateId,
                OfferId = serviceModel.OfferId,
                IpAddress = serviceModel.IpAddress,
            };

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

            apiRegistration.LeadId = lead.Id;

            await db.ApiRegistrations.AddAsync(apiRegistration);
            await db.Leads.AddAsync(lead);
            await db.SaveChangesAsync();

            return lead;
        }
    }
}
