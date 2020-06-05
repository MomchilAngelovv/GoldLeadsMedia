using GoldLeadsMedia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Database.Configurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.Property(p => p.PayPerClick).HasColumnType("decimal(18,4)");
            builder.Property(p => p.PayPerAction).HasColumnType("decimal(18,4)");
            builder.Property(p => p.PayPerLead).HasColumnType("decimal(18,4)");
        }
    }
}
