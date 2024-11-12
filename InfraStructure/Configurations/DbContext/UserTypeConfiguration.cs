using Domain.Models.Bid;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Configurations.DbContext
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<TenderUser>
    {
        public void Configure(EntityTypeBuilder<TenderUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Bid>()
                .WithOne(x => x.User)
                .HasForeignKey<Bid>(x => x.UserId);
        }
    }
}
