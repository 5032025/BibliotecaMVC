namespace PrimerSemana.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        public string Libro { get; set; }
        public string Autor { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaVencimiento { get; set; }

        
        public EstadoPrestamo Estado { get; set; }
    }

    public enum EstadoPrestamo
    {
        Activo,
        Vencido,
        Devuelto
    }
}