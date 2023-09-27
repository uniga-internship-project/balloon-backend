using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using uniga_internship_project.Data;
using uniga_internship_project.Services.AuthorizeSerivice.Requests;
using Microsoft.IdentityModel.JsonWebTokens;
namespace uniga_internship_project.Services.AuthorizeSerivice
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;

        public AuthorizeService(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        public async Task<string> Login(LoginRequest request)
        {
            var user = await _dataContext.User.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                throw new Exception();
            if (BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Password))
            {
                throw new Exception();
            }
            //token 
            string token = TokenGenerate(user);
            
            return token;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password, 10);
            var newUser = new User()
            {
                Email = request.Email,
                Password = hash,
            };
            await _dataContext.User.AddAsync(newUser);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        private string TokenGenerate(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
