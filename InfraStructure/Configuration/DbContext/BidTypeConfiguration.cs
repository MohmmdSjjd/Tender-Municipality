using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Bid;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Configuration.DbContext
{
    public class BidTypeConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(3)
                .HasColumnType("decimal(18,3)");

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.TenderId)
                .IsRequired();
        }
    }
}
