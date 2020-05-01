namespace GoldLeadsMedia.CoreApi.Services.Accesses
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;

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
            var accesses = this.db.Accesses.ToList();
            return accesses;
        }
    }
}
