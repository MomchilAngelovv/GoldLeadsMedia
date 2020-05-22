namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using System.Threading.Tasks;
    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;

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
                IpAddress = serviceModel.IpAddress
            };

            await db.ClickRegistrations.AddAsync(clickRegistration);
            await db.SaveChangesAsync();

            return clickRegistration;
        }
    }
}
