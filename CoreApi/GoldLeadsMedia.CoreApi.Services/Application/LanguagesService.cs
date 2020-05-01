namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class LanguagesService : ILanguagesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public LanguagesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Language> GetAll()
        {
            var languages = db.Languages.ToList();
            return languages;
        }
    }
}
