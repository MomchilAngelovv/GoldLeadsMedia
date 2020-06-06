namespace GoldLeadsMedia.CoreApi.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using GoldLeadsMedia.CoreApi.Services.Application.Common;

    public class PayTypesController : ApiController
    {
        private readonly IPaymentTypesService paymentTypesService;

        public PayTypesController(
            IPaymentTypesService paymentTypesService)
        {
            this.paymentTypesService = paymentTypesService;
        }

        public ActionResult<IEnumerable<object>> GetAll()
        {
            var paymentTypes = this.paymentTypesService
                .GetAll()
                .Select(paymentType => new  
                { 
                    paymentType.Id,
                    paymentType.Name
                })
                .ToList();

            return paymentTypes;
        }
    }
}
