using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kavitha.Models
{
  public class TravelRequest
    {
      [Key]
      public int RequestId { get; set; }

      [Required]
      [ForeignKey("Employee")]
      public int EmployeeId { get; set; }  // Reference to Employee

      public string Destination { get; set; }

      [Required]
      public DateTime StartDate { get; set; }

      [Required]
      public DateTime EndDate { get; set; }

      public string Purpose { get; set; }

      [Required]
      public string Status { get; set; } = "Pending"; // Default status

      public virtual User Employee { get; set; } // Navigation property
    }
}
