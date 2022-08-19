using System.ComponentModel.DataAnnotations;

namespace Bank.Client.Application.DTOs
{
    public class UpdateClientDto
    {
        [Required] public int Id { get; set; }
        [Required] public string Clave { get; set; }
    }
}
