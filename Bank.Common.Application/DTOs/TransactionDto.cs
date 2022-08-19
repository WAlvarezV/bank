namespace Bank.Common.Application.DTOs
{
    public class TransactionDto
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        public double SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public double Movimiento { get; set; }
        public double SaldoDisponible { get; set; }
    }
}
