using System.ComponentModel.DataAnnotations;

namespace kavitha.DTOS
{
  public class LoginRequest
  {
    //internal readonly string Email;

    [Required]
    [StringLength(100)]
    public string Username { get; set; }

    [Required]
    [StringLength(100)]
    public string Password { get; set; }
  }
}
