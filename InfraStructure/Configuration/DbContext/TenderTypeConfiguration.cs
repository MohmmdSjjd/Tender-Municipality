using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Tender;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Configuration.DbContext
{
    public class TenderTypeConfiguration : IEntityTypeConfiguration<Tender>
    {
        public void Configure(EntityTypeBuilder<Tender> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.OwnsOne(t => t.TenderDate, td =>
            {
                td.Property(d => d.StartDate)
                    .IsRequired()
                    .HasColumnName("StartDate");
                td.Property(d => d.EndDate)
                    .IsRequired()
                    .HasColumnName("EndDate");
            });

            builder.OwnsOne(t => t.Budget, b =>
            {
                b.Property(d => d.BigAmount)
                    .IsRequired()
                    .HasPrecision(3)
                    .HasColumnType("decimal(18,3)")
                    .HasColumnName("BigAmount");

                b.Property(d => d.SmallAmount)
                    .IsRequired()
                    .HasPrecision(3)
                    .HasColumnType("decimal(18,3)")
                    .HasColumnName("SmallAmount");
            });

            builder.HasMany(t => t.Bids)
                .WithOne()
                .HasForeignKey(b => b.TenderId);
        }
    }
}
