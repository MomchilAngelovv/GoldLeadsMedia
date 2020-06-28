namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class VerticalsService : IVerticalsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public VerticalsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Vertical> GetAll()
        {
            return db.Verticals
                .ToList();
        }
    }
}
