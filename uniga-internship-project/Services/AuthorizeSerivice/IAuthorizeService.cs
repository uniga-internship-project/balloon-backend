using uniga_internship_project.Models.Dto;
using uniga_internship_project.Services.Requests;

namespace uniga_internship_project.Services.AuthorizeSerivice
{
    public interface IAuthorizeService
    {
        Task<AuthorizeDto> Login(AuthorizeRequest request);
        Task<bool> Register(AuthorizeRequest request);
    }
}
