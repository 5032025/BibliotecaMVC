namespace PrimerSemana.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime ReservationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public ReserveStatus Status { get; set; }

        // Propiedades de apoyo para mostrar los datos en la vista si vienen mapeados
        public string Libro { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
    }

    public enum ReserveStatus
    {
        Active,
        Cancelled,
        Completed
    }
}