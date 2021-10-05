using CsvImporter.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImporter.Core.Mapping
{
    public class StockMovementMap : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("StockMovements");

            // Primary Key
            builder.HasKey(o => o.Id);

            // Properties
            builder.Property(o => o.Id).ValueGeneratedOnAdd().HasColumnType("int").HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(o => o.CreatedDate).HasColumnType("datetime");
            builder.Property(o => o.Date).HasColumnType("datetime");
            builder.Property(o => o.PointOfSale).HasMaxLength(255);
            builder.Property(o => o.Product).HasMaxLength(255);
            builder.Property(o => o.Stock).HasColumnType("smallint");

        }
    }
}
