namespace GoldLeadsMedia.CoreApi.Services.Languages
{
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models;
    
    public interface ILanguagesService
    {
        IEnumerable<Language> GetAll();
    }
}
