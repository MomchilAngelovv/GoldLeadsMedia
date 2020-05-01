using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.Verticals
{
    public class VerticalsService : IVerticalsService
    {
        private readonly GoldLeadsMediaDbContext db;

        public VerticalsService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Vertical> GetAll()
        {
            var verticals = this.db.Verticals.ToList();
            return verticals;
        }
    }
}
