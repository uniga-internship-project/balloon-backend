using Microsoft.AspNetCore.Mvc;
using uniga_internship_project.Services.Requests;

namespace uniga_internship_project.Services.UserService
{
    public interface IUserService
    {
        Task<User> GetUser(string token);
        Task<User> UpdateUser(int id);
    }
}
