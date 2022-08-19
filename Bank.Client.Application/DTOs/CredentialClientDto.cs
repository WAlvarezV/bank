using System.ComponentModel.DataAnnotations;

namespace Bank.Client.Application.DTOs
{
    public class CredentialClientDto
    {
        [Required] public string ClienteId { get; set; }
        [Required] public string Clave { get; set; }
    }
}
