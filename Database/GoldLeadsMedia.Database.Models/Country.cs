namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models.Common;

    public class Country : IEntityMetaData
    {
        public Country()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public string PhonePrefix { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Lead> Leads { get; set; }

        public DateTime CreatedOn { get; set ; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
