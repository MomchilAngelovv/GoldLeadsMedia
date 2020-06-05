namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class OfferOfferGroupConfiguration : IEntityTypeConfiguration<OfferOfferGroup>
    {
        public void Configure(EntityTypeBuilder<OfferOfferGroup> entity)
        {
            entity.HasKey(entity => new { entity.OfferId, entity.OfferGroupId });
        }
    }
}
