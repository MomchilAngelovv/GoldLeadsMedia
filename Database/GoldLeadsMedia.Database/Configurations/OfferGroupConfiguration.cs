namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class OfferGroupConfiguration : IEntityTypeConfiguration<OfferGroup>
    {
        public void Configure(EntityTypeBuilder<OfferGroup> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Name).IsRequired().HasMaxLength(100);
        }
    }
}
