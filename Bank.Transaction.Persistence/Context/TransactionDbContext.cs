using Bank.Transaction.Persistence.Context.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Bank.Transaction.Persistence.Context
{
    internal class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        #region DbSet
        public DbSet<Domain.Entities.Transaction> Transactions { get; set; }
        #endregion
    }
}
