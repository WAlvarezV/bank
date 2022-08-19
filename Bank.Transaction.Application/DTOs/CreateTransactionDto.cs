using Bank.Common.Application.Enum;
using System.ComponentModel.DataAnnotations;

namespace Bank.Transaction.Application.DTOs
{
    public class CreateTransactionDto
    {
        [Required] public TransactionEnum TipoCuenta { get; set; }
        [Required] public double Valor { get; set; }
        [Required] public int CuentaId { get; set; }
    }
}
