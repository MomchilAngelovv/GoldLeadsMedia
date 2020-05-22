using GoldLeadsMedia.CoreApi.Models.ServicesModels.OutputModels;
using GoldLeadsMedia.CoreApi.Services.Application.Common;
using GoldLeadsMedia.Database;
using GoldLeadsMedia.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Services.Application
{
    public class AffiliatesService : IAffiliatesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public AffiliatesService(GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Lead> GetLeadsBy(string affiliateId)
        {
            var leads = db.Leads.Where(lead => lead.ClickRegistration.AffiliateId == affiliateId);
            return leads;
        }

        public AffiliatesGetPaymentsByOutputServiceModel GetPaymentsBy(string affiliateId)
        {
            var availableMoney = this.db.Leads
                .Where(lead => lead.FtdBecameOn != null && lead.ClickRegistration.Affiliate.Id == affiliateId)
                .Sum(lead => lead.ClickRegistration.Offer.PayOut);

            var result = new AffiliatesGetPaymentsByOutputServiceModel
            {
                Available = availableMoney,
                Paid = 50 //Hardcoded need to iplement logic
            };

            return result;
        }
    }
}
