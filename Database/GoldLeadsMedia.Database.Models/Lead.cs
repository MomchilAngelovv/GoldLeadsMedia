namespace GoldLeadsMedia.Database.Models
{
    using System;

    using GoldLeadsMedia.Database.Models.Common;

    public class Lead : IEntityMetaData
    {
        public Lead()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //No idea why need to keep this field -> something Mike thinking -> ask Mike for more info if you are curiuos too
        public string Password { get; set; }

        public string CallStatus { get; set; }
        public string ConfirmedByManagerId { get; set; }
        public DateTime? FtdBecameOn { get; set; }
        public string IdInPartner { get; set; }

        public int CountryId { get; set; }
        public string PartnerId { get; set; }
        public string ClickId { get; set; }
        //AffiliateId and offerId are populated only when lead comes from API and there is no landing page. When lead comes from landing page only clickId is enough since click saves information for user and offer
        public string AffiliateId { get; set; }
        public string OfferId { get; set; }

        public virtual Country Country { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual Click Click { get; set; }
        public virtual GoldLeadsMediaUser Affiliate { get; set; }
        public virtual Offer Offer { get; set; }

        public DateTime CreatedOn { get ;set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
