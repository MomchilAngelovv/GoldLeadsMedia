using GoldLeadsMedia.CoreApi.Models.InputModels;
using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.CoreApi.Services.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Controllers
{
    public class PartnersController : ApiController
    {
        private readonly IPartnersService partnersService;

        public PartnersController(
            IPartnersService partnersService)
        {
            this.partnersService = partnersService;
        }

        public async Task<ActionResult<object>> Register(PartnersRegisterInputModel inputModel)
        {
            var serviceModel = new PartnersRegisterServiceModel
            {
                Name = inputModel.Name
            };

            var partner = await this.partnersService.RegisterAsync(serviceModel);
            return partner.Name;
        }
    }
}
