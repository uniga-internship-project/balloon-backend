using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uniga_internship_project.Services.SkillSevice;
using uniga_internship_project.Services.UserService;

namespace uniga_internship_project.Controllers
{
    [Route("api/v1/skill/")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService skillService;

        public SkillController(ISkillService skillService)
        {
            this.skillService = skillService;
        }
        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Get([FromRoute] int Id)
        {
            var result = await skillService.Get(Id);
            return Ok(result);
        }
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Create([FromBody]string name)
        {
            var result = await skillService.create(name);
            return Ok(result);
        }
    }
}
