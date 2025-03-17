//using System.ComponentModel.DataAnnotations;

//namespace kavitha.Models
//{
//  public class UserType
//  {
//    [Key]
//    public int Id { get; set; }
//    //public int UserTypeId { get; set; } // Ensure this is the correct primary key
//    public string Name { get; set; }
//    public string Description { get; set; }

//    public string RoleName { get; set; }



//    // Navigation property for User (optional)
//    public ICollection<User> Users { get; set; }
//  }
//}


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kavitha.Models
{
  public class UserType
  {
    [Key]
    public int Id { get; set; } // Primary Key

    [Required]
    [StringLength(100)]
    public string Name { get; set; } // Role Name (e.g., Admin, Employee, Manager)

    public string Description { get; set; }

    // ✅ Ensure Navigation Property is Initialized
    public ICollection<User> Users { get; set; } = new List<User>();
  }
}
