namespace PrimerSemana.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PublicationYear { get; set; }
        public string Description { get; set; }
        public List<int> AutorIds { get; set; } = new();
        public List<Autor>? Authors { get; set; }
        public List<int> CategoriaIds { get; set; } = new();

        public string Imagen { get; set; }
    }
}