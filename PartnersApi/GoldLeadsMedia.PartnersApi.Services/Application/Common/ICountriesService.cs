namespace GoldLeadsMedia.PartnersApi.Services.Application.Common
{
    using GoldLeadsMedia.Database.Models;

    public interface ICountriesService
    {
        Country GetBy(string name);
    }
}
