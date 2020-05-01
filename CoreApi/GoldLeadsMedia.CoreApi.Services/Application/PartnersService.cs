using GoldLeadsMedia.CoreApi.Models.ServiceModels;
using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
    public class PartnersService : IPartnersService
    {
        private readonly GoldLeadsMediaDbContext db;

        public PartnersService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public async Task<Partner> RegisterAsync(PartnersRegisterServiceModel serviceModel)
        {
            var partner = new Partner
            {
                Name = serviceModel.Name
            };

            await this.db.Partners.AddAsync(partner);
            await this.db.SaveChangesAsync();

            return partner;
        }
    }
}
