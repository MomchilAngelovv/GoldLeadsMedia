namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

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
            return this.db.Verticals
                .ToList();
        }
    }
}
