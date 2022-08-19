using Bank.Common.Application.Enum;
using System.ComponentModel.DataAnnotations;

namespace Bank.Account.Application.DTOs
{
    public class CreateAccountDto
    {
        [Required] public string NumeroCuenta { get; set; }
        [Required] public AccountEnum TipoCuenta { get; set; }
        [Required] public double SaldoInicial { get; set; }
        [Required] public int ClienteId { get; set; }
    }
}
