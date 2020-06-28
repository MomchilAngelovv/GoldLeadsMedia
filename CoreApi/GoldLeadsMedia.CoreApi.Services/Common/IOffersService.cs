namespace GoldLeadsMedia.CoreApi.Services.Common
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Models.Services.Input;

    public interface IOffersService
    {
        IEnumerable<Offer> GetAll(OffersGetAllFilterInputServiceModel filterServiceModel);
        Task<Offer> CreateAsync(OffersCreateInputServiceModel serviceModel);
        Offer GetBy(string id);
        Task<int> AssignLandingPagesAsync(OffersAssignLandingPagesInputServiceModel serviceModel);
        int CalculateFtdsPerOfferIdAndAffiliateId(string offerId, string affiliateId);
    }
}
