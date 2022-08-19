using Bank.Common.Application.Enum;
using System.ComponentModel.DataAnnotations;

namespace Bank.Client.Application.DTOs
{
    public class CreateClientDto
    {
        [Required] public string Identificacion { get; set; }
        [Required] public string NombreCompleto { get; set; }
        [Required] public GenderEnum Genero { get; set; }
        [Required] public int Edad { get; set; }
        [Required] public string Direccion { get; set; }
        [Required] public string Telefono { get; set; }
        [Required] public string ClienteId { get; set; }
        [Required] public string Clave { get; set; }
    }
}
