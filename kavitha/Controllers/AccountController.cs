using kavitha.Data;
using kavitha.Models;
using kavitha.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Threading.Tasks;
using kavitha.Helpers;

namespace kavitha.Controllers
{
  [Route("api/account")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly AppDbContext _context;
    private readonly string hashedPassword;
    private readonly JwtHelper _jwtHelper;
    private object token;

    //public AccountController(AppDbContext context)
    //{
    //  _context = context;
    //}
    public AccountController(AppDbContext context, JwtHelper jwtHelper)
    {
      _context = context;
      _jwtHelper = jwtHelper;
    }

    // 1️⃣ User Registration Endpoint
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
      if (_context.Users == null)
        return BadRequest("Database error: Users table not found.");

      // Check if the username already exists
      if (await _context.Users.AnyAsync(u => u.Username == request.Username))
        return BadRequest("Username already exists.");

      // Check if passwords match
      if (request.Password != request.confirmPassword)
        return BadRequest("Passwords do not match.");

      // Create new user with hashed password
      var newUser = new User
      {
        Username = request.Username,
        Email = request.Email,
        UserType = request.UserType,
        PasswordHash = HashPassword(request.Password)
      };

      // Save user to database
      _context.Users.Add(newUser);
      await _context.SaveChangesAsync();

      return Ok("User registered successfully.");
    }

    // 2️⃣ User Login Endpoint
    // POST: api/account/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
      var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginRequest.Username);

      if (user == null || !VerifyPassword(user.PasswordHash, loginRequest.Password))
      {
        return Unauthorized("Invalid username or password.");
      }

      var token = _jwtHelper.GenerateJwtToken(user);

      return Ok(new { token });
    }


    // 🔹 Hash Password
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

    // 🔹 Verify Password
    private bool VerifyPassword(string hashedPassword, string inputPassword)
    {
      return hashedPassword == HashPassword(inputPassword);
    }

    // 🔹 Generate JWT Token (Dummy Implementation)
    private string GenerateJwtToken(User user)
    {
      return "generated-jwt-token";  // Placeholder - Implement JWT logic later
    }
  }
}
