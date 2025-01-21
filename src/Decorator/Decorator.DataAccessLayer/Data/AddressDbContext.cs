using Decorator.ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Decorator.DataAccessLayer.Data
{
    public class AddressDbContext : DbContext
    {
        public AddressDbContext(DbContextOptions<AddressDbContext> options)
        : base(options)
        {
        }
        public DbSet<AddressModel> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressModel>().ToTable("Addresses");

        }



    }
}
