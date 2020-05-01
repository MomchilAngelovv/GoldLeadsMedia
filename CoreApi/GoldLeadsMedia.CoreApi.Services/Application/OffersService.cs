namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.InputModels;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Models.ServiceModels;

    public class OffersService : IOffersService
    {
        private readonly GoldLeadsMediaDbContext db;

        public OffersService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public async Task<int> AssignLandingPagesAsync(OffersAssignLandingPagesInputModel inputModel)
        {
            var offersLandingPages = new List<OfferLandingPage>();

            foreach (var landingPageId in inputModel.LandingPageIds)
            {
                var offerLandingPage = new OfferLandingPage
                {
                    OfferId = inputModel.OfferId,
                    LandingPageId = landingPageId
                };

                offersLandingPages.Add(offerLandingPage);
            }

            await db.OffersLandingPages.AddRangeAsync(offersLandingPages);
            await db.SaveChangesAsync();

            return offersLandingPages.Count;
        }
        public async Task<Offer> CreateAsync(OffersCreateServiceModel inputModel)
        {
            var offer = new Offer
            {
                Number = inputModel.Number,
                Name = inputModel.Name,
                VerticalId = inputModel.VerticalId,
                AccessId = inputModel.AccessId,
                CountryId = inputModel.CountryId,
                Description = inputModel.Description,
                ActionFlow = inputModel.ActionFlow,
                CreatedByManagerId = inputModel.CreatedByManagerId,
                LanguageId = inputModel.LanguageId,
                PayTypeId = inputModel.PayTypeId,
                TargetDeviceId = inputModel.TargetDeviceId,
                PayPerClick = inputModel.PayPerClick,
                PayOut = inputModel.PayOut
            };

            await db.Offers.AddAsync(offer);
            await db.SaveChangesAsync();

            return offer;
        }
        public IEnumerable<Offer> GetAll()
        {
            var offers = db.Offers.ToList();
            return offers;
        }
        public Offer GetBy(string id)
        {
            var offer = db.Offers
                .FirstOrDefault(x => x.Id == id);

            return offer;
        }
    }
}
