namespace GoldLeadsMedia.CoreApi.Services.OfferClicks
{
    using System;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.InputModels.OfferClicks;
    using System.Threading.Tasks;

    public class OfferClicksService : IOfferClicksService
    {
        private readonly GoldLeadsMediaDbContext db;

        public OfferClicksService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public async Task<OfferClick> RegisterClickAsync(RegisterOfferClickInputModel inputModel)
        {
            var offerClick = new OfferClick
            {
                OfferId = inputModel.OfferId,
                LandingPageId = inputModel.LandingPageId,
                UserId = inputModel.UserId
            };

            await this.db.OfferClicks.AddAsync(offerClick);
            await this.db.SaveChangesAsync();

            return offerClick;
        }
    }
}
