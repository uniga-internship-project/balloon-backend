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
        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetUser([FromRoute] int id)
        //{
        //    var result = await _userService.GetUser(id);
        //    return Ok(result);
        //}
    }
}
