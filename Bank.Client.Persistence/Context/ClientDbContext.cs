using Bank.Client.Domain.Entities;
using Bank.Client.Persistence.Context.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Bank.Client.Persistence.Context
{
    internal class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        #region DbSet
        public DbSet<Person> People { get; set; }
        public DbSet<Domain.Entities.Client> Clients { get; set; }
        #endregion
    }
}
