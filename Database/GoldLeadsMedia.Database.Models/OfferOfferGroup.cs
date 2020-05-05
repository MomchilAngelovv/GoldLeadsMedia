namespace GoldLeadsMedia.Database.Models
{
    public class OfferOfferGroup
    {
        public string OfferId { get; set; }
        public virtual Offer Offer { get; set; }

        public string OfferGroupId { get; set; }
        public virtual OfferGroup OfferGroup { get; set; }
    }
}
