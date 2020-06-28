namespace GoldLeadsMedia.CoreApi.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Common;

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

            await db.LandingPages.AddAsync(landingPage);
            await db.SaveChangesAsync();

            return landingPage;
        }

        public IEnumerable<LandingPage> GetAll()
        {
            return db.LandingPages
                .ToList();
        }

        public LandingPage GetBy(string id)
        {
            return db.LandingPages
                .FirstOrDefault(landingPage => landingPage.Id == id);
        }
    }
}
