using System.Reflection;
using Domain.Models.Bid;
using Domain.Models.Tender;
using Domain.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Data
{
    public class TenderContext:IdentityDbContext<TenderUser>
    {
        public TenderContext(DbContextOptions<TenderContext> options) : base(options)
        {
            //var connectionString = Database.GetConnectionString();
            //Database.EnsureCreatedAsync();
        }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Bid?> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base class implementation 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
