using GoldLeadsMedia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Database.Configurations
{
    public class GoldLeadsMediaRoleConfiguration : IEntityTypeConfiguration<GoldLeadsMediaRole>
    {
        public void Configure(EntityTypeBuilder<GoldLeadsMediaRole> builder)
        {

        }
    }
}
