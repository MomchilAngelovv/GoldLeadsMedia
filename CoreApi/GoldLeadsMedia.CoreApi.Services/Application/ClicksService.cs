namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Threading.Tasks;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    
    public class ClicksService : IClicksService
    {
        private readonly GoldLeadsMediaDbContext db;

        public ClicksService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
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
