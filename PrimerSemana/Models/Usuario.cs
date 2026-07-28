namespace PrimerSemana.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Ubicacion { get; set; }
        public int Edad { get; set; }

        public string Rol { get; set; } 

        public string Password { get; set; }
    }
}