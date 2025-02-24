using System.ComponentModel.DataAnnotations;

namespace kavitha.DTOS
{
  public class ChangePasswordRequest
  {
    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }
  }
}
