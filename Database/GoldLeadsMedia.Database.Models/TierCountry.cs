using GoldLeadsMedia.Database.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoldLeadsMedia.Database.Models
{
    public class TierCountry : IEntityMetaData
    {
        public TierCountry()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
