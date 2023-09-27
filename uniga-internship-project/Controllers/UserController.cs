using Microsoft.AspNetCore.Mvc;
using uniga_internship_project.Services.Requests;
using uniga_internship_project.Services.UserService;

namespace uniga_internship_project.Controllers
{
    [Route("api/v1/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Users>>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser([FromBody] CreateUserRequest request) 
        {
            var result = await _userService.CreateUser(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser([FromRoute] int id)
        {
            var result = await _userService.GetUser(id);
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id) 
        {
            var result = await (_userService.DeleteUser(id));
            return Ok(result);
        }
    }
}
