using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;
using uniga_internship_project.Services.AuthorizeSerivice;
using uniga_internship_project.Services.AuthorizeSerivice.Requests;

namespace uniga_internship_project.Controllers
{
    [Route("api/authorize/")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthorizeController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginRequest request)
        {
            var result = await _authorizeService.Login(request);
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterRequest request)
        {
            var result = await _authorizeService.Register(request);
            return Ok(result);
        }

    }
}
