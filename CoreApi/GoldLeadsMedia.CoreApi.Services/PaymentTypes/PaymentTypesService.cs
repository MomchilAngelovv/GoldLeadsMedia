using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.PaymentTypes
{
    public class PaymentTypesService : IPaymentTypesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public PaymentTypesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PaymentType> GetAll()
        {
            var paymentTypes = this.db.PaymentTypes.ToList();
            return paymentTypes;
        }
    }
}
