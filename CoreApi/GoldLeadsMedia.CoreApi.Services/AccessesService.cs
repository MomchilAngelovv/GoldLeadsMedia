namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class AccessesService : IAccessesService
    {
        private readonly GoldLeadsMediaDbContext db;
        public AccessesService
            (GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Access> GetAll()
        {
            return db.Accesses
                .ToList();
        }
    }
}
