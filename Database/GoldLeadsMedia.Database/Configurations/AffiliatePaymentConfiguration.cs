using GoldLeadsMedia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Database.Configurations
{
    public class AffiliatePaymentConfiguration : IEntityTypeConfiguration<AffiliatePayment>
    {
        public void Configure(EntityTypeBuilder<AffiliatePayment> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(p => p.Amount).HasColumnType("decimal(18,4)");
            entity.Property(p => p.Reason).IsRequired().HasMaxLength(100);
            entity.Property(p => p.InvoiceNumber).HasMaxLength(100);
            entity.Property(p => p.AffiliateId).IsRequired();
        }
    }
}
