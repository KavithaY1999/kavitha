using System.ComponentModel.DataAnnotations;

namespace kavitha.Models
{
  public class Userlist
  {
    [Key]
    public int UserId { get; set; }

    [Required]
    [StringLength(100)]
    public string Username { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; } // Store the hashed password

    [Required]
    public string UserType { get; set; }

    public string Status { get; set; } // New property for user status
  }
}
