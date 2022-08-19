using Bank.Client.Domain.Entities;

namespace Bank.Client.Persistence
{
    internal interface IUnitOfWork
    {
        Repository<Domain.Entities.Client> Clients { get; }
        Repository<Person> People { get; }
        Task<bool> SaveAsync();
    }
}
