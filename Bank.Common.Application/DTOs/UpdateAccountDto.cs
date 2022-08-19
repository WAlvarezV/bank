using System.ComponentModel.DataAnnotations;

namespace Bank.Common.Application.DTOs
{
    public class UpdateAccountDto
    {
        [Required] public int AccountId { get; set; }
        [Required] public double Valor { get; set; }
    }
}
