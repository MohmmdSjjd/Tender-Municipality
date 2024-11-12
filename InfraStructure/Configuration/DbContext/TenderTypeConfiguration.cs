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

            builder.OwnsOne(t => t.TenderDate, td =>
            {
                td.Property(d => d.StartDate)
                    .HasColumnName("StartDate");
                td.Property(d => d.EndDate)
                    .HasColumnName("EndDate");
            });

            builder.OwnsOne(t => t.Budget, b =>
            {
                b.Property(d => d.BigAmount)
                    .HasColumnType("decimal(18,3)")
                    .HasColumnName("BigAmount");

                b.Property(d => d.SmallAmount)
                    .HasColumnType("decimal(18,3)")
                    .HasColumnName("SmallAmount");
            });

            builder.HasMany(t => t.Bids)
                .WithOne()
                .HasForeignKey(b => b.TenderId);
        }
    }
}
