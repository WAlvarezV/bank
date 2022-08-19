namespace Bank.Account.Persistence
{
    internal interface IUnitOfWork
    {
        Repository<Domain.Entities.Account> Accounts { get; }
        Task<bool> SaveAsync();
    }
}
