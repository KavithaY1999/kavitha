using kavitha.Models;
using System.ComponentModel.DataAnnotations;

namespace kavitha.DTOS
{
  public class UpdateProfileRequest
  {
    [Required]
    [StringLength(100)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public UserType UserType { get; set; }
  }
}
