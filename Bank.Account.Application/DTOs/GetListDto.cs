using System.ComponentModel.DataAnnotations;

namespace Bank.Account.Application.DTOs
{
    public class GetListDto
    {
        [Required] public DateTime StartDate { get; set; }
        [Required] public DateTime EndDate { get; set; }
    }
}
