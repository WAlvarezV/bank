using Bank.Common.Application.Enum;

namespace Bank.Common.Application.DTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NumeroCuenta { get; set; }
        public AccountEnum TipoCuenta { get; set; }
        public double Saldo { get; set; }
        public bool Estado { get; set; }
        public IList<TransactionDto> Movimientos { get; set; }
    }
}
