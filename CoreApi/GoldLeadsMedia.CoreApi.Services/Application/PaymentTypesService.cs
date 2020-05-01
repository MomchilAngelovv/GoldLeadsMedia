using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
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
            var paymentTypes = db.PaymentTypes.ToList();
            return paymentTypes;
        }
    }
}
