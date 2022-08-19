using System.ComponentModel.DataAnnotations;

namespace Bank.Transaction.Application.DTOs
{
    public class GetListDto
    {
        [Required] public int ClientId { get; set; }
        [Required] public DateTime StartDate { get; set; }
        [Required] public DateTime EndDate { get; set; }
    }
}
