namespace Bank.Transaction.Persistence
{
    internal interface IUnitOfWork
    {
        Repository<Domain.Entities.Transaction> Transactions { get; }
        Task<bool> SaveAsync();
    }
}
