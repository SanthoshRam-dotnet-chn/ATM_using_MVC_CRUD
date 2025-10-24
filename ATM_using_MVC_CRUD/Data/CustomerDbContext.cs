using ATM_using_MVC_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace ATM_using_MVC_CRUD.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.AccountNumber)
                .IsUnique();
        }
    }
}
