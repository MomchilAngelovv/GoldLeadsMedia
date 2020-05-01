namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.PaymentTypes;
    using GoldLeadsMedia.CoreApi.Models.ResponseModels;

    public class PaymentTypesController : ApiController
    {
        private readonly IPaymentTypesService paymentTypesService;

        public PaymentTypesController(
            IPaymentTypesService paymentTypesService)
        {
            this.paymentTypesService = paymentTypesService;
        }

        public ActionResult<IEnumerable<PaymentTypeResponseModel>> GetAll()
        {
            var paymentTypes = this.paymentTypesService
                .GetAll()
                .Select(paymentType => new PaymentTypeResponseModel 
                { 
                    Id = paymentType.Id,
                    Name = paymentType.Name
                })
                .ToList();

            return paymentTypes;
        }
    }
}
