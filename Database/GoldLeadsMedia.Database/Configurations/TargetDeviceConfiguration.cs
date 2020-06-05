using GoldLeadsMedia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Database.Configurations
{
    public class TargetDeviceConfiguration : IEntityTypeConfiguration<TargetDevice>
    {
        public void Configure(EntityTypeBuilder<TargetDevice> builder)
        {

        }
    }
}
