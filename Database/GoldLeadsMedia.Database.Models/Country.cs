namespace GoldLeadsMedia.Database.Models
{
    using System;

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

        public DateTime CreatedOn { get; set ; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
