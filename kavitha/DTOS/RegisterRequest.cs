using kavitha.Models;
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
    public UserType UserType { get; set; }


    [Required]
    public string confirmPassword { get; set; }

    [Required]
    //public int UserTypeId { get; set; }

  }
}


//using System.ComponentModel.DataAnnotations;

//namespace kavitha.DTOS
//{
//  public class RegisterRequest
//  {
//    [Required]
//    [StringLength(100)]
//    public string Username { get; set; }

//    [Required]
//    [StringLength(100)]
//    [EmailAddress]
//    public string Email { get; set; }

//    [Required]
//    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
//    public string Password { get; set; }

//    [Required]
//    [Compare("Password", ErrorMessage = "Passwords do not match.")]
//    public string ConfirmPassword { get; set; }

//    [Required(ErrorMessage = "UserTypeId is required.")]
//    public int UserTypeId { get; set; }  // ✅ Store only the foreign key
//  }
//}
