namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class TierCountryConfiguration : IEntityTypeConfiguration<CountryTier>
    {
        public void Configure(EntityTypeBuilder<CountryTier> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Name).IsRequired().HasMaxLength(100);
        }
    }
}
