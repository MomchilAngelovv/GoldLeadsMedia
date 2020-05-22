using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
    public class LandingPagesService : ILandingPagesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public LandingPagesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public async Task<LandingPage> CreateAsync(string name, string url)
        {
            var landingPage = new LandingPage
            {
                Name = name,
                Url = url
            };

            await this.db.LandingPages.AddAsync(landingPage);
            await this.db.SaveChangesAsync();

            return landingPage;
        }

        public IEnumerable<LandingPage> GetAll()
        {
            var landingPages = this.db.LandingPages.ToList();
            return landingPages;
        }

        public LandingPage GetBy(string id)
        {
            var landingPage = this.db.LandingPages.FirstOrDefault(landingPage => landingPage.Id == id);
            return landingPage;
        }
    }
}
