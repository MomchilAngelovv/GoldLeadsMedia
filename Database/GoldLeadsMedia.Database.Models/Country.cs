namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GoldLeadsMedia.Database.Models.Common;

    public class Country : IEntityMetaData
    {
        public Country()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key] 
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string IsoCode { get; set; }
        [MaxLength(100)]
        public string PhonePrefix { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Lead> Leads { get; set; }

        public DateTime CreatedOn { get; set ; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
