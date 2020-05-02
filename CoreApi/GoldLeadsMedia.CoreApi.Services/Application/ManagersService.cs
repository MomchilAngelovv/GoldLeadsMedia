using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
    public class ManagersService : IManagersService
    {
        private readonly GoldLeadsMediaDbContext db;
        private readonly UserManager<GoldLeadsMediaUser> userManager;

        public ManagersService(
            GoldLeadsMediaDbContext db, 
            UserManager<GoldLeadsMediaUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<GoldLeadsMediaUser>> GetAffiliatesByAsync(string managerId)
        {
            var affiliates = await this.userManager.GetUsersInRoleAsync("Affiliate");
            var managerAffiliates = affiliates.Where(user => user.ManagerId == managerId);

            return affiliates;
        }
    }
}
