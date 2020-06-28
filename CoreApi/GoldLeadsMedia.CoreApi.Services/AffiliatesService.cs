namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;
    using GoldLeadsMedia.CoreApi.Services.Common;
    using GoldLeadsMedia.CoreApi.Models.Services.Output;

    public class AffiliatesService : IAffiliatesService
    {
        private readonly GoldLeadsMediaDbContext db;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public AffiliatesService(
            GoldLeadsMediaDbContext db,
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<string> CreateOrUpdateTrackerConfiguration(AffiliatesCreateOrUpdateTrackerConfigurationInputServiceModel serviceModel)
        {
            var trackerConfiguration = this.db.TrackerConfigurations
                .FirstOrDefault(trackerConfiguration => trackerConfiguration.AffiliateId == serviceModel.AffiliateId);

            if (trackerConfiguration == null)
            {
                var newTrackerConfiguration = new TrackerConfiguration
                {
                    AffiliateId = serviceModel.AffiliateId,
                    LeadPostbackUrl = serviceModel.LeadPostbackUrl,
                    FtdPostbackUrl = serviceModel.FtdPostbackUrl,
                };

                await this.db.TrackerConfigurations.AddAsync(newTrackerConfiguration);
            }
            else
            {
                trackerConfiguration.LeadPostbackUrl = serviceModel.LeadPostbackUrl;
                trackerConfiguration.FtdPostbackUrl = serviceModel.FtdPostbackUrl;

                this.db.TrackerConfigurations.Update(trackerConfiguration);
            }

            await this.db.SaveChangesAsync();
            return "Done";
        }
        public async Task<IEnumerable<GoldLeadsMediaUser>> GetAllAsync()
        {
            return await this.userManager
                .GetUsersInRoleAsync("Affiliate");
        }
        public IEnumerable<Lead> GetLeadsBy(string affiliateId)
        {
            return this.db.Leads
                .Where(lead => lead.ClickRegistration.AffiliateId == affiliateId)
                .ToList();
        }
        public IEnumerable<Offer> GetOffersBy(string affiliateId)
        {
            return this.db.Offers
                .Where(offer => offer.ApiRegistrations.Any(apiRegistration => apiRegistration.AffiliateId == affiliateId) || offer.ClickRegistrations.Any(clickRegistration => clickRegistration.AffiliateId == affiliateId))
                .ToList();
        }
        public AffiliatesGetPaymentsStatusByOutputServiceModel GetPaymentsStatusBy(string affiliateId)
        {
            //TODO: This will get money ONLY FOR PPA -> NEED TO INCLUDE EVERY POSSIBLE AFFILATE PAY (PPL/PPC) BUT FOR NOW WE WORK ONLY CPA
            var affiliate = this.db.Users
                .SingleOrDefault(user => user.Id == affiliateId);

            if (affiliate == null)
            {
                //TODO: Think for logic if no user is found but still not sure if its possible 
            }

            var totalEarned = this.db.Leads
                .Where(lead => lead.FtdBecameOn != null && (lead.ClickRegistration.Affiliate.Id == affiliate.Id || lead.ApiRegistration.AffiliateId == affiliate.Id))
                .Sum(lead => 
                     lead.ClickRegistration.Offer.PayPerAction.GetValueOrDefault() + 
                     lead.ClickRegistration.Offer.PayPerClick.GetValueOrDefault() +
                     lead.ClickRegistration.Offer.PayPerLead.GetValueOrDefault() +
                     lead.ApiRegistration.Offer.PayPerAction.GetValueOrDefault() +
                     lead.ApiRegistration.Offer.PayPerClick.GetValueOrDefault() +
                     lead.ApiRegistration.Offer.PayPerLead.GetValueOrDefault());

            var result = new AffiliatesGetPaymentsStatusByOutputServiceModel
            {
                TotalEarned = totalEarned,
                TotalPaid = 0
            };

            return result;
        }
        public TrackerConfiguration GetTrackerSettings(string affiliateId)
        {
            return this.db.TrackerConfigurations
                .SingleOrDefault(trackerConfiguration => trackerConfiguration.AffiliateId == affiliateId);
        }
    }
}
