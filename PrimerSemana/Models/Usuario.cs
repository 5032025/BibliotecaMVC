namespace PrimerSemana.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Propiedad calculada o directa para que la vista muestre el nombre completo fácilmente
        public string Name => $"{FirstName} {LastName}";

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;

        // Propiedad que faltaba para el rol
        public string Role { get; set; } = "Usuario";
    }

    public class LoginViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}