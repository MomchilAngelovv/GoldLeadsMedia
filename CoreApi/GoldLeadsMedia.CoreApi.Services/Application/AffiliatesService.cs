namespace GoldLeadsMedia.CoreApi.Services.Application
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using GoldLeadsMedia.Database;
    using GoldLeadsMedia.Database.Models;
    using GoldLeadsMedia.CoreApi.Services.Application.Common;
    using GoldLeadsMedia.CoreApi.Models.ServicesModels.OutputModels;

    public class AffiliatesService : IAffiliatesService
    {
        private readonly GoldLeadsMediaDbContext db;

        public AffiliatesService(
            GoldLeadsMediaDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Lead> GetLeadsBy(string affiliateId)
        {
            var leads = db.Leads
                .Where(lead => lead.ClickRegistration.AffiliateId == affiliateId)
                .ToList();

            return leads;
        }

        public AffiliatesGetPaymentsStatusByOutputServiceModel GetPaymentsStatusBy(string affiliateId)
        {
            //TODO: This will get money ONLY FOR PPA -> NEED TO INCLUDE EVERY POSSIBLE AFFILATE PAY (PPL/PPC)
            var affiliate = this.db.Users
                .SingleOrDefault(user => user.Id == affiliateId);

            if (affiliate == null)
            {
                //TODO: Think for logic if no user is found but still not sure if its possible 
            }

            var totalEarned = this.db.Leads
                .Where(lead => lead.FtdBecameOn != null && (lead.ClickRegistration.Affiliate.Id == affiliate.Id || lead.ApiRegistration.AffiliateId == affiliate.Id))
                .Sum(lead => 
                    lead.ClickRegistration.Offer.PayPerAction.GetValueOrDefault() + 
                    lead.ClickRegistration.Offer.PayPerClick.GetValueOrDefault() +
                    lead.ClickRegistration.Offer.PayPerLead.GetValueOrDefault() +
                    lead.ApiRegistration.Offer.PayPerAction.GetValueOrDefault() +
                    lead.ApiRegistration.Offer.PayPerClick.GetValueOrDefault() +
                    lead.ApiRegistration.Offer.PayPerLead.GetValueOrDefault());

            var totalPaid = affiliate.AffiliatePayments
                .Sum(affilatePayment => affilatePayment.Amount);

            var result = new AffiliatesGetPaymentsStatusByOutputServiceModel
            {
                TotalEarned = totalEarned,
                TotalPaid = totalPaid
            };

            return result;
        }
    }
}
