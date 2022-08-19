using Bank.Common.Application.Enum;
using Bank.Common.Domain.Entities;

namespace Bank.Transaction.Domain.Entities
{
    internal class Transaction : BaseEntity
    {
        public int AccountId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TransactionEnum TransactionType { get; set; }
        public double Value { get; set; }
        public double InitialBalance { get; set; }
        public double Balance { get; set; }
        public bool State { get; set; }
    }
}
