﻿using GoldLeadsMedia.CoreApi.Models.CoreApiModels;
using GoldLeadsMedia.CoreApi.Models.InputModels;
using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.CoreApi.Services.Partners.Common;
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
        private readonly IServiceProvider serviceProvider;

        public PartnersController(
            IPartnersService partnersService,
            IServiceProvider serviceProvider)
        {
            this.partnersService = partnersService;
            this.serviceProvider = serviceProvider;
        }

        public ActionResult<IEnumerable<object>> GetAll()
        {
            var partners = this.partnersService
                .GetAll();

            var response = partners
                .Select(partner => new
                {
                    partner.Id,
                    partner.Name
                })
                .ToList();

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Register(PartnersRegisterInputModel inputModel)
        {
            var serviceModel = new PartnersRegisterInputServiceModel
            {
                Name = inputModel.Name
            };

            var partner = await this.partnersService.RegisterAsync(serviceModel);

            var response = new
            {
                partner.Name
            };

            return response;
        }

        [HttpPost("{partnerId}/SendLeads")]
        public async Task<ActionResult<int>> SendLeads(string partnerId, PartnersSendLeadsInputModel inputModel)
        {
            var partner = this.partnersService.GetBy(partnerId);

            if (partner == null)
            {
                //TODO LOGIC IF no partner is find
            }

            var partnerType = typeof(IPartner)
                .Assembly
                .GetTypes()
                .SingleOrDefault(type => type.IsClass && type.IsPublic && type.Name.ToLower().StartsWith(partner.Name.ToLower()) && type.Name.EndsWith("Partner"));

            var partnerInstance = this.serviceProvider.GetService(partnerType) as IPartner;

            var errorCount = await partnerInstance.SendLeadsAsync(inputModel.LeadIds, partner.Id);
            return errorCount;
        }

        [HttpPost("/Scan")]
        public ActionResult<int> FtdScan()
        {
            var partnerTypes = typeof(IPartner)
                .Assembly
                .GetTypes()
                .Where(type => type.IsClass && type.IsPublic && type.Name.EndsWith("Partner"));

            var ftdCount = 0;

            foreach (var partnerType in partnerTypes)
            {
                var partnerInstance = this.serviceProvider.GetService(partnerType) as IPartner;

                var partnerFtds = partnerInstance.FtdScan();
                ftdCount += partnerFtds;
            }

            return ftdCount;
        }
    }
}
