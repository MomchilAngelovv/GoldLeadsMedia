namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using System.Linq;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;
    using GoldLeadsMedia.CoreApi.Services.Common;

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
            return db.ClickRegistrations
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
