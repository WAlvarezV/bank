namespace Bank.Common.Application.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string NombreCompleto { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string ClienteId { get; set; }
        public IList<AccountDto> Cuentas { get; set; }
    }
}
