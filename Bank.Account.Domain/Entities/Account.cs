using Bank.Common.Application.Enum;
using Bank.Common.Domain.Entities;

namespace Bank.Account.Domain.Entities
{
    internal class Account : BaseEntity
    {
        public int ClientId { get; set; }
        public string Number { get; set; }
        public AccountEnum AccountType { get; set; }
        public double Balance { get; set; }
        public bool State { get; set; }
    }
}
