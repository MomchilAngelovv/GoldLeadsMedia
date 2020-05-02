﻿using GoldLeadsMedia.CoreApi.Services.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Controllers
{
    public class ManagersController : ApiController
    {
        private readonly IManagersService managersService;

        public ManagersController(
            IManagersService managersService)
        {
            this.managersService = managersService;
        }

        [HttpGet("{managerId}/Affiliates")]
        public async Task<ActionResult<IEnumerable<object>>> GetManagerLeads(string managerId)
        {
            var managersAffiliates = await this.managersService.GetAffiliatesByAsync(managerId);

            var response = managersAffiliates
                .Where(affiliate => affiliate.ManagerId == managerId)
                .Select(affiliate => new
                {
                    affiliate.Id,
                    affiliate.UserName,
                    IsDeleted = affiliate.DeletedOn != null,
                    affiliate.Skype,
                    IsVip = false, //TODO implement logic
                    affiliate.Experience
                })
                .ToList();

            return response;
        }
    }
}
