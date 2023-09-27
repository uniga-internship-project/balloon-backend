using BCrypt.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Net;
using uniga_internship_project.Data;
using uniga_internship_project.Models;
using uniga_internship_project.Services.Requests;

namespace uniga_internship_project.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Users> CreateUser(CreateUserRequest request)
        {
            var hash =  BCrypt.Net.BCrypt.HashPassword(request.Password, 10);
            var newUser = new Users() 
            {
                Email = request.Email,
                Password = hash,
            };
            await _dataContext.Users.AddAsync(newUser);
            await _dataContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);
            if (user == null) 
            {
                throw new Exception();
            }
            _dataContext.Remove(user);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            var users =  await _dataContext.Users.ToListAsync();
            return users;
        }

        public async Task<Users> GetUser(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);
            if (user == null) 
            {
                throw new Exception("Data Notfound!");
            }
            return user;
        }

        public Task<Users> UpdateUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}