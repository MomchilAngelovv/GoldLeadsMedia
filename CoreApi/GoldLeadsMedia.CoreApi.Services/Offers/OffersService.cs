namespace GoldLeadsMedia.CoreApi.Services.Offers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.InputModels.Offers;

    public class OffersService : IOffersService
    {
        private readonly GoldLeadsMediaDbContext db;

        public OffersService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public async Task<int> AssignLandingPagesAsync(AssignLandingPagesInputModel inputModel)
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

            await this.db.OffersLandingPages.AddRangeAsync(offersLandingPages);
            await this.db.SaveChangesAsync();

            return offersLandingPages.Count;
        }
        public async Task<Offer> CreateAsync(CreateInputModel inputModel)
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
                CreatedBy = inputModel.CreatedBy,
                LanguageId = inputModel.LanguageId,
                PaymentTypeId = inputModel.PaymentTypeId,
                TargetDeviceId = inputModel.TargetDeviceId,
                EarningPerClick = inputModel.EarningPerClick,
                PayOut = inputModel.PayOut
            };

            await this.db.Offers.AddAsync(offer);
            await this.db.SaveChangesAsync();

            return offer;
        }
        public IEnumerable<Offer> GetAll()
        {
            var offers = this.db.Offers.ToList();
            return offers;
        }
        public Offer GetBy(string id)
        {
            var offer = this.db.Offers
                .FirstOrDefault(x => x.Id == id);

            return offer;
        }
    }
}
