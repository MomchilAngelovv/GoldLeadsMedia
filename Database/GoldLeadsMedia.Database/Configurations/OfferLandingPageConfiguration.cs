namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class OfferLandingPageConfiguration : IEntityTypeConfiguration<OfferLandingPage>
    {
        public void Configure(EntityTypeBuilder<OfferLandingPage> entity)
        {
            entity.HasKey(entity => new { entity.OfferId, entity.LandingPageId });
        }
    }
}
