using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using uniga_internship_project.Data;
using uniga_internship_project.Services.AuthorizeSerivice.TokenService;

namespace uniga_internship_project.Services.AuthorizeSerivice.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;
        public TokenService(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
        }

        public async Task<string> GenerateToken(User user)
        {
            var roleId = await _dataContext.Role.FindAsync(user.RoleId);
            var role = roleId.Name;
            // Create claims with user information
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role),

            // Add more claims as needed (e.g., roles, user ID, etc.)
        };

            // Get the secret key from configuration
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSetting:Token"]));

            // Create signing credentials using HMAC-SHA256
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

            // Create a JWT token with claims, expiration, and signing credentials
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(3), 
                signingCredentials: signingCredentials
            );

            // Serialize the JWT token to a string
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
