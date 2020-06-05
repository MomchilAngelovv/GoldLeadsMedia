using GoldLeadsMedia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Database.Configurations
{
    public class OfferOfferGroupConfiguration : IEntityTypeConfiguration<OfferOfferGroup>
    {
        public void Configure(EntityTypeBuilder<OfferOfferGroup> builder)
        {
            builder.HasKey(e => new { e.OfferId, e.OfferGroupId });

        }
    }
}
