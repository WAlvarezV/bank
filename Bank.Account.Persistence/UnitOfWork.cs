using Bank.Account.Persistence.Context;

namespace Bank.Account.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AccountDbContext _context;
        public UnitOfWork(AccountDbContext context) => _context = context;
        public Repository<Domain.Entities.Account> Accounts => new(_context);
        public async Task<bool> SaveAsync() => (await _context.SaveChangesAsync()) > 0;
    }
}
