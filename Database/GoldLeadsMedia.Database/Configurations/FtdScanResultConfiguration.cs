using GoldLeadsMedia.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Database.Configurations
{
    public class FtdScanResultConfiguration : IEntityTypeConfiguration<FtdScanResult>
    {
        public void Configure(EntityTypeBuilder<FtdScanResult> entity)
        {
            entity.HasKey(entity => entity.Id);
        }
    }
}
