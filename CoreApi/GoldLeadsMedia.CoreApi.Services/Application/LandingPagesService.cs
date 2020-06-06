namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;

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
            return this.db.LandingPages
                .ToList();
        }

        public LandingPage GetBy(string id)
        {
            return this.db.LandingPages
                .FirstOrDefault(landingPage => landingPage.Id == id);
        }
    }
}
