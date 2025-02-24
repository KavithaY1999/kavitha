using System.ComponentModel.DataAnnotations;

namespace kavitha.DTOS
{
  public class RegisterRequest
  {
    [Required]
    [StringLength(100)]
    public string Username { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    public string Password { get; set; }

    [Required]
    public string UserType { get; set; }


    [Required]
    public string confirmPassword { get; set; }


  }
}
