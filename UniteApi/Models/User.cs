using System.ComponentModel.DataAnnotations;

namespace UniteApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }  // We slaan de hash op, niet het wachtwoord zelf!
    }
}
