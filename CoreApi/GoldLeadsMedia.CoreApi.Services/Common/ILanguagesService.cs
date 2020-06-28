namespace GoldLeadsMedia.CoreApi.Services.Common
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;

    public interface ILanguagesService
    {
        IEnumerable<Language> GetAll();
    }
}
