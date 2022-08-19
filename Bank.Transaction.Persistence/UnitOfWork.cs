using Bank.Transaction.Persistence.Context;

namespace Bank.Transaction.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly TransactionDbContext _context;
        public UnitOfWork(TransactionDbContext context) => _context = context;
        public Repository<Domain.Entities.Transaction> Transactions => new(_context);
        public async Task<bool> SaveAsync() => (await _context.SaveChangesAsync()) > 0;
    }
}
