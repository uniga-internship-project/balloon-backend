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
using uniga_internship_project.Services.AuthorizeSerivice.TokenService;

namespace uniga_internship_project.Services.AuthorizeSerivice
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        public AuthorizeService(DataContext dataContext, IConfiguration configuration, ITokenService tokenService)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _tokenService = tokenService;
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

            string test = await _tokenService.GenerateToken(user);

            return test;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password, 10);
            var newUser = new User()
            {
                Email = request.Email,
                Password = hash,
                RoleId = 1,
            };
            await _dataContext.User.AddAsync(newUser);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
