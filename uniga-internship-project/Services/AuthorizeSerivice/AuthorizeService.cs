using Microsoft.EntityFrameworkCore;
using uniga_internship_project.Data;
using uniga_internship_project.Models.Dto;
using uniga_internship_project.Services.AuthorizeSerivice.Requests;
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
        public async Task<AuthorizeDto> Login(LoginRequest request)
        {
            var user = await _dataContext.User.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                throw new Exception("User Not found!");
            if (BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.Password))
            {
                throw new Exception("E-mail or password incorrect!");
            }

            string token = await _tokenService.GenerateToken(user);

            var dto = new AuthorizeDto()
            {
                Token = token,
                UserId = user.Id,
            };

            return dto;
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
            var isRoleExist = await _dataContext.Role.ToListAsync();
            if (isRoleExist.Count == 0) 
            {
                var user = new Role()
                {
                    Id = 1,
                    Name = "User"
                };
                var admin = new Role()
                {
                    Id = 2,
                    Name = "Admin"
                };
                await _dataContext.Role.AddAsync(user);
                await _dataContext.Role.AddAsync(admin);
                await _dataContext.SaveChangesAsync();
            }
            await _dataContext.User.AddAsync(newUser);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
