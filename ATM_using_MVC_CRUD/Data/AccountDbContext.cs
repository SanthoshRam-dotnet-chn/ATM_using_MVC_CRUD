using ATM_using_MVC_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace ATM_using_MVC_CRUD.Data
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }

        public DbSet<AccountTransaction> Transactions { get; set; }
    }
}
