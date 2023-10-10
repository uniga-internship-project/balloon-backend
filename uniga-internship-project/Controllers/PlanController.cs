using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uniga_internship_project.Services.Criteria;
using uniga_internship_project.Services.PlansService;
using uniga_internship_project.Services.SkillSevice;
using uniga_internship_project.Services.UserService;

namespace uniga_internship_project.Controllers
{
    [Route("api/v1/plan/")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlansService plansService;

        public PlanController(IPlansService plansService)
        {
            this.plansService = plansService;
        }
        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Get([FromRoute] int Id)
        {
            var result = await plansService.Get(Id);
            return Ok(result);
        }
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Create([FromBody] string name , string description)
        {
            var result = await plansService.create(name,description);
            return Ok(result);
        }
        [HttpPost("Search")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Plan>>> Search([FromBody] SearchPlansCriteria request)
        {
            var result = await plansService.Search(request);
            return Ok(result);
        }
    }
}
