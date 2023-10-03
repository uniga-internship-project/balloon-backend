using uniga_internship_project.Models.Dto;
using uniga_internship_project.Services.AuthorizeSerivice.Requests;

namespace uniga_internship_project.Services.AuthorizeSerivice
{
    public interface IAuthorizeService
    {
        Task<AuthorizeDto> Login(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
}
