namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Name).IsRequired().HasMaxLength(100);
            entity.Property(entity => entity.Description).IsRequired().HasMaxLength(400);
            entity.Property(entity => entity.Number).IsRequired().HasMaxLength(100);
            entity.Property(entity => entity.ActionFlow).IsRequired().HasMaxLength(400);
            entity.Property(entity => entity.PayPerAction).HasColumnType("decimal(18,4)");
            entity.Property(entity => entity.PayPerLead).HasColumnType("decimal(18,4)");
            entity.Property(entity => entity.PayPerClick).HasColumnType("decimal(18,4)");
        }
    }
}
