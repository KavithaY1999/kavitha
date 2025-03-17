//using System.ComponentModel.DataAnnotations;

//namespace kavitha.Models
//{
//  public class User
//  {
//    [Key]
//    public int UserId { get; set; }

//    [Required]
//    [StringLength(100)]
//    public string Username { get; set; }

//    [Required]
//    [StringLength(100)]
//    public string Email { get; set; }

//    [Required]
//    public string PasswordHash { get; set; } // Store the hashed password

//    [Required]
//    //public string confirmPassword { get; set; }

//    //[Required]
//    //public UserType UserType { get; set; }
//    //public object UserTypeId { get; internal set; }
//    //public object Status { get; internal set; }
//    public int UserTypeId { get; set; }  // ✅ Foreign key column
//    public string UserType { get; set; } // ✅ Navigation Property

//    // Foreign key reference
//    //[Required]
//    //public int UserTypeId { get; set; }

//  }
//}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    // ✅ Define Foreign Key Correctly
    [Required]
    public int UserTypeId { get; set; }

    // ✅ Navigation Property
    public UserType UserType { get; set; }
  }
}
