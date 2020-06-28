namespace GoldLeadsMedia.AffiliatesApi.Services.Common
{
    using GoldLeadsMedia.Database.Models;

    public interface ICountriesService
    {
        Country GetBy(string name);
        bool ExistsCheckBy(string name);
    }
}
