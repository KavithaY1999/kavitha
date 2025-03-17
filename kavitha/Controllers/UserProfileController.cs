using kavitha.DTOS;
using kavitha.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace kavitha.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserProfileController : ControllerBase
  {
    private readonly Data.AppDbContext _context; // ✅ Use actual DbContext

    public UserProfileController(Data.AppDbContext context) // ✅ Inject DbContext via constructor
    {
      _context = context;
    }

    // ✅ Get User Profile by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserProfile(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound("User not found.");
      }

      return Ok(new
      {
        user.UserId,
        user.Username,
        user.Email,
        user.UserType
      });
    }

    // ✅ Update User Profile
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateProfileRequest request)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound("User not found.");
      }

      user.Username = request.Username;
      user.Email = request.Email;
      user.UserType = request.UserType;

      _context.Users.Update(user);
      await _context.SaveChangesAsync(); // ✅ Now SaveChangesAsync() works!

      return Ok( new{message= "Profile updated successfully." });
    }

    // ✅ Change Password
    [HttpPut("change-password/{id}")]
    public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequest request)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound("User not found.");
      }

      if (!VerifyPassword(user.PasswordHash, request.OldPassword))
      {
        return BadRequest("Incorrect old password.");
      }

      user.PasswordHash = HashPassword(request.NewPassword);
      _context.Users.Update(user);
      await _context.SaveChangesAsync(); // ✅ Now SaveChangesAsync() works!

      return Ok("Password changed successfully.");
    }

    // DELETE: api/account/delete/{id}
    //[HttpDelete("delete/{id,Username}")]
    //public async Task<IActionResult> DeleteUser(int id, string Username)
    //{
    //  var user = await _context.Users.FindAsync(id);
    //  var user1 = await _context.Users.FindAsync(Username);
    //  if (user == null || user1==null)
    //  {
    //    return NotFound();
    //  }

    //  _context.Users.Remove(user);
    //  await _context.SaveChangesAsync();

    //  return Ok();
    //}

    [HttpDelete("delete/{id}/{username}")]
    public async Task<IActionResult> DeleteUser(int id, string username)
    {
      try
      {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id && u.Username == username);

        if (user == null)
        {
          return NotFound(new { message = "User not found." });
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User deleted successfully." });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { message = "An error occurred while deleting the user.", error = ex.Message });
      }
    }

    // 🔹 Helper: Hash Password
    private string HashPassword(string password)
    {
      byte[] salt = new byte[128 / 8];
      using (var rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(salt);
      }

      return Convert.ToBase64String(KeyDerivation.Pbkdf2(
          password: password,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA256,
          iterationCount: 10000,
          numBytesRequested: 256 / 8));
    }

    // 🔹 Helper: Verify Password
    private bool VerifyPassword(string hashedPassword, string inputPassword)
    {
      return hashedPassword == HashPassword(inputPassword);
    }
  }
}
