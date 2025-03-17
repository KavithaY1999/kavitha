using kavitha.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace kavitha.Helpers
{
  public class JwtHelper
  {
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    // 🔹 Generate JWT Token
    public string GenerateJwtToken(User user)
    {
      if (user == null) throw new ArgumentNullException(nameof(user));

      // Create claims (you can add more claims if needed)
      var claims = new[]
      {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

      // Get the secret key from appsettings.json and validate
      var secretKey = _configuration["Jwt:Key"];
      if (string.IsNullOrEmpty(secretKey))
      {
        throw new InvalidOperationException("JWT SecretKey is missing in configuration.");
      }

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      // Define the token expiration time (e.g., 1 hour)
      var expires = DateTime.UtcNow.AddHours(1);

      // Create the JWT token
      var token = new JwtSecurityToken(
          issuer: _configuration["JwtSettings:Issuer"],
          audience: _configuration["JwtSettings:Audience"],
          claims: claims,
          expires: expires,
          signingCredentials: creds
      );

      // Return the JWT token as a string
      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}

//using kavitha.Models;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace kavitha.Helpers
//{
//  public class JwtHelper
//  {
//    private readonly IConfiguration _configuration;

//    public JwtHelper(IConfiguration configuration)
//    {
//      _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
//    }

//    // 🔹 Generate JWT Token with UserType (Role)
//    public string GenerateJwtToken(User user)
//    {
//      if (user == null) throw new ArgumentNullException(nameof(user));

//      // Ensure user has a valid UserType
//      string userRole = user.UserType?.RoleName ?? "Employee"; // Default role if null

//      // Create claims for the token
//      var claims = new[]
//      {
//          new Claim(JwtRegisteredClaimNames.Sub, user.Username),
//          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//          new Claim(ClaimTypes.Role, userRole) // Add user role
//      };

//      // Retrieve secret key from configuration
//      var secretKey = _configuration["Jwt:Key"];
//      if (string.IsNullOrEmpty(secretKey))
//      {
//        throw new InvalidOperationException("JWT SecretKey is missing in configuration.");
//      }

//      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
//      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//      // Define token expiration (1 hour)
//      var expires = DateTime.UtcNow.AddHours(1);

//      // Create the JWT token
//      var token = new JwtSecurityToken(
//          issuer: _configuration["Jwt:Issuer"],
//          audience: _configuration["Jwt:Audience"],
//          claims: claims,
//          expires: expires,
//          signingCredentials: creds
//      );

//      // Return token as a string
//      return new JwtSecurityTokenHandler().WriteToken(token);
//    }
//  }
//}
