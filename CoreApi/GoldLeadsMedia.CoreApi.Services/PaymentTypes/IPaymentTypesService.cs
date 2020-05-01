using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.PaymentTypes
{
    public interface IPaymentTypesService
    {
        IEnumerable<PaymentType> GetAll();
    }
}
