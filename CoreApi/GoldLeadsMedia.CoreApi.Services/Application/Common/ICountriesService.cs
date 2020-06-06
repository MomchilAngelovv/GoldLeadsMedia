namespace GoldLeadsMedia.CoreApi.Services.Application.Common
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;

    public interface ICountriesService
    {
        IEnumerable<Country> GetAll();
        Country GetBy(string name);
    }
}
