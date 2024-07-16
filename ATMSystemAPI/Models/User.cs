namespace ATMSystemAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty; // Varsayılan değer atandı
        public string Password { get; set; } = string.Empty; // Varsayılan değer atandı
        public string Token { get; set; } = string.Empty;
    }
}
