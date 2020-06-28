namespace GoldLeadsMedia.AffiliatesApi.Services.Common
{
    using GoldLeadsMedia.Database.Models;

    public interface IOffersService
    {
        Offer GetBy(string offerId);
        bool ExistsCheckBy(string offerId);
    }
}
