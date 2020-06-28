namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Common;

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
            return db.Languages
                .ToList();
        }
    }
}
