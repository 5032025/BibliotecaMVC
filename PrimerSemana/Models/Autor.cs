namespace PrimerSemana.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public string Nationality { get; set; }
        public List<Libro>? Books { get; set; }

        public string Imagen { get; set; }

        public AuthorStatus Status { get; set; }
    }

    public enum AuthorStatus
    {
        Active,
        Inactive
    }
}