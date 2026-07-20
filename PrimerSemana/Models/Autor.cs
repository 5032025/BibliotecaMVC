namespace PrimerSemana.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Nacionalidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string FotoUrl { get; set; }
        public EstadoAutor Estado { get; set; } = EstadoAutor.Activo;

    }

    public enum EstadoAutor 
    {
        Activo, Inactivo
    }
}