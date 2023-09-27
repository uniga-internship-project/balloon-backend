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
        public async Task<User> GetUser(string token)
        {
            var user = await _dataContext.User.FindAsync(token);
            if (user == null) 
            {
                throw new Exception("Data Notfound!");
            }
            return user;
        }

        public Task<User> UpdateUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}