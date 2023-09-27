using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Asn1.Ocsp;
using uniga_internship_project.Data;
using uniga_internship_project.Services.AuthorizeSerivice.Requests;

namespace uniga_internship_project.Services.AuthorizeSerivice
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly DataContext _dataContext;

        public AuthorizeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<string> Login(LoginRequest request)
        {
            var user = await _dataContext.Users.FindAsync(request.Email);
            if (user == null)
                throw new Exception();
            if (!BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Password))
            {
                throw new Exception();
            }
            //token 
            throw new NotImplementedException();
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password, 10);
            var newUser = new Users()
            {
                Email = request.Email,
                Password = hash,
            };
            await _dataContext.Users.AddAsync(newUser);
            await _dataContext.SaveChangesAsync();
            return true;
        }

    }
}
