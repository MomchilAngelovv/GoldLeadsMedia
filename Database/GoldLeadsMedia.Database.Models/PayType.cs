namespace GoldLeadsMedia.Database.Models
{
    using System;

    using GoldLeadsMedia.Database.Models.Common;

    public class PayType : IEntityMetaData
    {
        public PayType()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
