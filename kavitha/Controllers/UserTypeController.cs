using kavitha.Data;
using kavitha.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kavitha.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Roles = "Admin")] // 🔒 Only Admins can modify roles
  public class UserTypeController : ControllerBase
  {
    private readonly AppDbContext _context;

    public UserTypeController(AppDbContext context)
    {
      _context = context;
    }

    // ✅ Get All UserTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserType>>> GetUserTypes()
    {
      var userTypes = await _context.UserTypes.ToListAsync();
      return Ok(userTypes);
    }

    // ✅ Get UserType by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<UserType>> GetUserType(int id)
    {
      var userType = await _context.UserTypes.FindAsync(id);
      if (userType == null)
      {
        return NotFound(new { message = "UserType not found." });
      }
      return Ok(userType);
    }

    // ✅ Create UserType
    [HttpPost]
    public async Task<ActionResult<UserType>> CreateUserType([FromBody] UserTypeDTO userTypeDTO)
    {
      if (_context.UserTypes.Any(ut => ut.Name == userTypeDTO.Name))
      {
        return BadRequest(new { message = "UserType already exists." });
      }

      var userType = new UserType
      {
        Name = (string)userTypeDTO.Name, // ✅ No casting needed
        Description = (string)userTypeDTO.Description // ✅ No casting needed
      };

      _context.UserTypes.Add(userType); // ✅ Removed `object value =`
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetUserType), new { id = userType.Id }, userType);
    }

    // ✅ Update UserType
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserType(int id, [FromBody] UserTypeDTO userTypeDTO)
    {
      var userType = await _context.UserTypes.FindAsync(id);
      if (userType == null) // ✅ Fixed condition
      {
        return NotFound(new { message = "UserType not found." });
      }

      userType.Name = (string)userTypeDTO.Name;
      userType.Description = (string)userTypeDTO.Description;

      _context.UserTypes.Update(userType);
      await _context.SaveChangesAsync();

      return Ok(new { message = "UserType updated successfully." });
    }

    // ✅ Delete UserType
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserType(int id)
    {
      var userType = await _context.UserTypes.FindAsync(id);
      if (userType == null)
      {
        return NotFound(new { message = "UserType not found." });
      }

      _context.UserTypes.Remove(userType);
      await _context.SaveChangesAsync();

      return Ok(new { message = "UserType deleted successfully." });
    }
  }
}
