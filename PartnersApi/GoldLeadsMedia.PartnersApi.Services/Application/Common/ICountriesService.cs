namespace GoldLeadsMedia.AffiliatesApi.Services.Application.Common
{
    using GoldLeadsMedia.Database.Models;

    public interface ICountriesService
    {
        Country GetBy(string name);
        bool ExistsCheckBy(string name);
    }
}
