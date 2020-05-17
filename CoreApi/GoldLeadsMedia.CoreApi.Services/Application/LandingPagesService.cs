using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

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
