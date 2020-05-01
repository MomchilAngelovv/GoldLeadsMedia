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

        public async Task<Click> RegisterAsync(ClicksRegisterServiceModel serviceModel)
        {
            var click = new Click
            {
                OfferId = serviceModel.OfferId,
                LandingPageId = serviceModel.LandingPageId,
                AffiliateId = serviceModel.AffiliateId,
                IpAddress = serviceModel.IpAddress
            };

            await db.Clicks.AddAsync(click);
            await db.SaveChangesAsync();

            return click;
        }
    }
}
