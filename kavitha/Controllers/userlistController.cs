using kavitha.Data;
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
    public class userlistController : ControllerBase
    {
    private readonly AppDbContext _context;
    public userlistController(AppDbContext context)
    {
      _context = context;
    }
    //private bool VerifyPassword(string hashedPassword, string inputPassword)
    //{
    //return hashedPassword == HashPassword(inputPassword);
    //}
    // GET: api/account/users
    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
      var users = await _context.Users.ToListAsync();
      return Ok(users);
    }

    // PUT: api/account/edit
    [HttpPut("edit")]
    public async Task<IActionResult> EditUser(User user)
    {
      var existingUser = await _context.Users.FindAsync(user.UserId);
      if (existingUser == null)
      {
        return NotFound();
      }


      existingUser.Username = user.Username;
      existingUser.Email = user.Email;
      //existingUser.UserType = user.UserType;
      //existingUser.Status = user.Status;
      // Update other properties as needed

      _context.Users.Update(existingUser);
      await _context.SaveChangesAsync();

      return Ok();
    }
    // GET: api/account/user/{id}
    [HttpGet("user/{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }
      return Ok(user);
    }


    // DELETE: api/account/delete/{id}
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      var user = await _context.Users.FindAsync(id);

      if (user == null)
      {
        return NotFound();
      }

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

      return Ok(new { message = " User deleted successfully." });
    }
    //// POST: api/userlist/register
    //[HttpPost("register")]
    //public async Task<IActionResult> RegisterUser([FromBody] User user)
    //{
    //  if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrEmpty(user.UserType))
    //  {
    //    return BadRequest("All fields are required.");
    //  }

    //  // Check if the email already exists
    //  var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
    //  if (existingUser != null)
    //  {
    //    return Conflict("A user with this email already exists.");
    //  }

    //  try
    //  {
    //    // Hash the password
    //    //user.PasswordHash = HashPassword(user.PasswordHash);

    //    // Add user to the database
    //    _context.Users.Add(user);
    //    await _context.SaveChangesAsync();

    //    return Ok(new { message = "User registered successfully!" });
    //  }
    //  catch (Exception ex)
    //  {
    //    // Log the exception (optional)
    //    return StatusCode(500, new { message = "An error occurred while registering the user.", error = ex.Message });
    //  }
    //}



    private string Userpassword(string password)
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
  }
  }

