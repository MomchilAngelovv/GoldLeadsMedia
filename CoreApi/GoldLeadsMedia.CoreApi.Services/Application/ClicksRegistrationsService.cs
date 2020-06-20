namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using System.Linq;

    public class ClicksRegistrationsService : IClicksRegistrationsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public ClicksRegistrationsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public ClickRegistration GetBy(string clickRegistrationId)
        {
            return this.db.ClickRegistrations
                .SingleOrDefault(clickRegistration => clickRegistration.Id == clickRegistrationId);
        }

        public async Task<ClickRegistration> RegisterAsync(ClicksRegisterInputServiceModel serviceModel)
        {
            var clickRegistration = new ClickRegistration
            {
                OfferId = serviceModel.OfferId,
                LandingPageId = serviceModel.LandingPageId,
                AffiliateId = serviceModel.AffiliateId,
                IpAddress = serviceModel.IpAddress,
                AffiliateTrackerClickId = serviceModel.AffiliateTrackerClickId
            };

            await db.ClickRegistrations.AddAsync(clickRegistration);
            await db.SaveChangesAsync();

            return clickRegistration;
        }
    }
}
