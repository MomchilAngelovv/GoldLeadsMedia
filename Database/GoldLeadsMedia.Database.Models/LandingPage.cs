namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models.Common;

    public class LandingPage : IEntityMetaData
    {
        public LandingPage()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<OfferLandingPage> OffersLandingPages { get; set; }

        public DateTime CreatedOn { get ; set ; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
