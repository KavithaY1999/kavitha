using System.ComponentModel.DataAnnotations;

namespace kavitha.Models
{
  public class User
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
    //public string confirmPassword { get; set; }

    //[Required]
    public string UserType { get; set; }
    //public object Status { get; internal set; }

  }
}
