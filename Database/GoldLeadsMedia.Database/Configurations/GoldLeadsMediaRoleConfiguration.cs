namespace GoldLeadsMedia.Database.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GoldLeadsMedia.Database.Models;

    public class GoldLeadsMediaRoleConfiguration : IEntityTypeConfiguration<GoldLeadsMediaRole>
    {
        public void Configure(EntityTypeBuilder<GoldLeadsMediaRole> entity)
        {

        }
    }
}
