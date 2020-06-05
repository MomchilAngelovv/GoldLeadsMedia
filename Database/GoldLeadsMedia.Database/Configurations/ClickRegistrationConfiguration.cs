namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class ClickRegistrationConfiguration : IEntityTypeConfiguration<ClickRegistration>
    {
        public void Configure(EntityTypeBuilder<ClickRegistration> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.IpAddress).IsRequired().HasMaxLength(100);
            entity.Property(entity => entity.OfferId).IsRequired();
            entity.Property(entity => entity.LandingPageId).IsRequired();
            entity.Property(entity => entity.AffiliateId).IsRequired();
        }
    }
}
