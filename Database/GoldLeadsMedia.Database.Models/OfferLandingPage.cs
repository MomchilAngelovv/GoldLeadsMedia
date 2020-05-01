namespace GoldLeadsMedia.Database.Models
{
    public class OfferLandingPage
    {
        public string OfferId { get; set; }
        public string LandingPageId { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual LandingPage LandingPage { get; set; }
    }
}
