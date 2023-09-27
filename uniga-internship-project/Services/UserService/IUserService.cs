using Microsoft.AspNetCore.Mvc;
using uniga_internship_project.Services.Requests;

namespace uniga_internship_project.Services.UserService
{
    public interface IUserService
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUser(int id);
        Task<Users> CreateUser(CreateUserRequest request);
        Task<Users> UpdateUser(int id);
        Task<bool> DeleteUser(int id);
    }
}
