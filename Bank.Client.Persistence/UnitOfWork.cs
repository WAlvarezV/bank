using Bank.Client.Domain.Entities;
using Bank.Client.Persistence.Context;

namespace Bank.Client.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ClientDbContext _context;
        public UnitOfWork(ClientDbContext context) => _context = context;
        public Repository<Domain.Entities.Client> Clients => new(_context);
        public Repository<Person> People => new(_context);
        public async Task<bool> SaveAsync() => (await _context.SaveChangesAsync()) > 0;
    }
}
