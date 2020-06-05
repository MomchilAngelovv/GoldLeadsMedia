namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class SendLeadErrorConfiguration : IEntityTypeConfiguration<SendLeadError>
    {
        public void Configure(EntityTypeBuilder<SendLeadError> entity)
        {
            entity.HasKey(entity => entity.Id);
            entity.Property(entity => entity.Message).IsRequired().HasMaxLength(400);
            entity.Property(entity => entity.LeadId).IsRequired();
            entity.Property(entity => entity.BrokerId).IsRequired();
        }
    }
}
