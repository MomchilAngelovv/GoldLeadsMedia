namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Common;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

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

            var landingPagesToRemove = db.OffersLandingPages.Where(ofp => ofp.OfferId == serviceModel.OfferId);
            db.OffersLandingPages.RemoveRange(landingPagesToRemove);

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

        public int CalculateFtdsPerOfferIdAndAffiliateId(string offerId, string affiliateId)
        {
            var affiliateLeadsIdsByApiRegistration = db.ApiRegistrations
                .Where(apiRegistration => apiRegistration.OfferId == offerId && apiRegistration.AffiliateId == affiliateId)
                .Select(apiRegistration => apiRegistration.Id)
                .ToList();

            return db.Leads.Where(lead => lead.FtdBecameOn.HasValue && affiliateLeadsIdsByApiRegistration.Contains(lead.ApiRegistrationId)).Count();
        }

        public async Task<Offer> CreateAsync(OffersCreateInputServiceModel inputModel)
        {
            var offer = new Offer
            {
                Number = inputModel.Number,
                Name = inputModel.Name,
                VerticalId = inputModel.VerticalId,
                AccessId = inputModel.AccessId,
                CountryTierId = inputModel.CountryTierId,
                Description = inputModel.Description,
                ActionFlow = inputModel.ActionFlow,
                LanguageId = inputModel.LanguageId,
                PayTypeId = inputModel.PayTypeId,
                TargetDeviceId = inputModel.TargetDeviceId,
                PayPerAction = inputModel.PayPerAction,
                PayPerLead = inputModel.PayPerLead,
                PayPerClick = inputModel.PayPerClick,
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
                offers = db.OffersOfferGroups.Where(oog => oog.OfferGroupId == filterServiceModel.GroupId).Select(oog => oog.Offer);
            }

            return offers;
        }
        public Offer GetBy(string id)
        {
            return db.Offers
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
