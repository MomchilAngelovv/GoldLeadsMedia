namespace GoldLeadsMedia.Web.Models.Managers
{
    using System;

    public class AffiliateDetailsPayment
    {
        public string Id { get; set; }
        public string OfferName { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayedOn { get; set; }
        public string PayedByUserName { get; set; }
    }
}
