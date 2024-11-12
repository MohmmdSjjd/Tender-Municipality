using Domain.Models.Tender;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Configurations.DbContext
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
                .WithOne(x=>x.Tender)
                .HasForeignKey(b => b.TenderId);
        }
    }
}
