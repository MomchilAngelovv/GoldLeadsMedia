﻿namespace GoldLeadsMedia.Database.Models
{
    using System;
    using System.Collections.Generic;

    using GoldLeadsMedia.Database.Models.Common;

    public class Vertical : IEntityMetaData
    {
        public Vertical()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
