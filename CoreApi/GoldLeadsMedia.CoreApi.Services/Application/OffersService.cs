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

        public async Task<int> AssignLandingPagesAsync(OffersAssignLandingPagesInputServiceModel serviceModel)
        {
            var offersLandingPages = new List<OfferLandingPage>();

            var landingPagesToRemove = this.db.OffersLandingPages.Where(ofp => ofp.OfferId == serviceModel.OfferId);
            this.db.OffersLandingPages.RemoveRange(landingPagesToRemove);

            foreach (var landingPageId in serviceModel.LandingPageIds)
            {
                var offerLandingPage = new OfferLandingPage
                {
                    OfferId = serviceModel.OfferId,
                    LandingPageId = landingPageId
                };

                offersLandingPages.Add(offerLandingPage);
            }

            await db.OffersLandingPages.AddRangeAsync(offersLandingPages);
            await db.SaveChangesAsync();

            return offersLandingPages.Count;
        }
        public async Task<Offer> CreateAsync(OffersCreateInputServiceModel inputModel)
        {
            var offer = new Offer
            {
                Number = inputModel.Number,
                Name = inputModel.Name,
                VerticalId = inputModel.VerticalId,
                AccessId = inputModel.AccessId,
                TierCountryId = inputModel.TierCountryId,
                Description = inputModel.Description,
                ActionFlow = inputModel.ActionFlow,
                LanguageId = inputModel.LanguageId,
                PayTypeId = inputModel.PayTypeId,
                TargetDeviceId = inputModel.TargetDeviceId,
                PayPerClick = inputModel.PayPerClick,
                PayPerAction = inputModel.PayOut,
                Information = $"[Created by: {inputModel.CreatedByManagerId}]"
            };

            await db.Offers.AddAsync(offer);
            await db.SaveChangesAsync();

            return offer;
        }
        public IEnumerable<Offer> GetAll(OffersGetAllFilterInputServiceModel filterServiceModel)
        {
            var offers = db.Offers.AsQueryable();

            if (filterServiceModel.NumberOrName != null)
            {
                offers = offers.Where(offer => offer.Number.Contains(filterServiceModel.NumberOrName) | offer.Name.Contains(filterServiceModel.NumberOrName));
            }

            if (filterServiceModel.VerticalId != null)
            {
                offers = offers.Where(offer => offer.VerticalId == filterServiceModel.VerticalId);
            }

            if (filterServiceModel.PayTypeId != null)
            {
                offers = offers.Where(offer => offer.PayTypeId == filterServiceModel.PayTypeId);
            }

            if (filterServiceModel.TargetDeviceId != null)
            {
                offers = offers.Where(offer => offer.TargetDeviceId == filterServiceModel.TargetDeviceId);
            }

            if (filterServiceModel.AccessId != null)
            {
                offers = offers.Where(offer => offer.AccessId == filterServiceModel.AccessId);
            }

            if (filterServiceModel.GroupId != null)
            {
                offers = this.db.OffersOfferGroups.Where(oog => oog.OfferGroupId == filterServiceModel.GroupId).Select(oog => oog.Offer);
            }

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
