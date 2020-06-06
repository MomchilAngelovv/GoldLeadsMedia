namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class PaymentTypesService : IPaymentTypesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public PaymentTypesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PayType> GetAll()
        {
            return this.db.PaymentTypes
                .ToList();
        }
    }
}
