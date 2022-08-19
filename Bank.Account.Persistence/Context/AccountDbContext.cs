using Bank.Account.Persistence.Context.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Bank.Account.Persistence.Context
{
    internal class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        #region DbSet
        public DbSet<Domain.Entities.Account> Accounts { get; set; }
        #endregion
    }
}
