namespace GoldLeadsMedia.CoreApi.Services.Languages
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;

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
            var languages = this.db.Languages.ToList();
            return languages;
        }
    }
}
