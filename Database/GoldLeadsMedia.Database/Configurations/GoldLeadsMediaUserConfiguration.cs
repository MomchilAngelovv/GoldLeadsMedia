using GoldLeadsMedia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Database.Configurations
{
    public class GoldLeadsMediaUserConfiguration : IEntityTypeConfiguration<GoldLeadsMediaUser>
    {
        public void Configure(EntityTypeBuilder<GoldLeadsMediaUser> builder)
        {

        }
    }
}
