using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectBlockChain.Helpers.Security.JWT
{
  public interface IJwtService
  {
    public string CreateToken(IEnumerable<Claim> claims);
    public ClaimsPrincipal GetUserPrincipalFromToken(string token);
  }

  public class JwtService : IJwtService
  {
    private readonly IConfiguration _configuration;
    public JwtService(IConfiguration configuration)
    => _configuration = configuration;

    public string CreateToken(IEnumerable<Claim> claims)
    {
      var tokenHandler = new JwtSecurityTokenHandler();

      var secretKey = _configuration["AppSetting:SecretKey"];
      var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
      var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature);

      var tokenOption = new JwtSecurityToken(
          issuer: null,
          audience: null,
          claims: claims,
          expires: DateTime.Now.AddMinutes(10),
          signingCredentials: signingCredentials
      );

      string tokenString = tokenHandler.WriteToken(tokenOption);
      return tokenString;
    }

    public ClaimsPrincipal GetUserPrincipalFromToken(string token)
    {
      var handler = new JwtSecurityTokenHandler();
      var validationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSetting:SecretKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
      };

      var pricipal = handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
      var jwtToken = validatedToken as JwtSecurityToken;

      if (jwtToken == null) throw new SecurityTokenException("Invalid Token");

      return pricipal;
    }
  }
}
