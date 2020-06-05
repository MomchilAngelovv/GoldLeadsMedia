namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class AffiliatePaymentConfiguration : IEntityTypeConfiguration<AffiliatePayment>
    {
        public void Configure(EntityTypeBuilder<AffiliatePayment> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Amount).HasColumnType("decimal(18,4)");
            entity.Property(entity => entity.Reason).IsRequired().HasMaxLength(100);
            entity.Property(entity => entity.InvoiceNumber).HasMaxLength(100);
            entity.Property(entity => entity.AffiliateId).IsRequired();
        }
    }
}
