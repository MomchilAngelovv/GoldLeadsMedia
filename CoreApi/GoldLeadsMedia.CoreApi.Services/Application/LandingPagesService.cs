using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
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
            var landingPages = db.LandingPages.ToList();
            return landingPages;
        }
    }
}
