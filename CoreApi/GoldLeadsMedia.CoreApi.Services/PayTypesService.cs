namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Common;

    public class PayTypesService : IPayTypesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public PayTypesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PayType> GetAll()
        {
            return db.PayTypes
                .ToList();
        }
    }
}
