using uniga_internship_project.Services.Criteria;

namespace uniga_internship_project.Services.SkillSevice
{
    public interface ISkillService
    {
        Task<Skill> Get(int Id);
        Task<bool> create(string name);
        Task<List<Skill>> Search (SearchSkillCriteria request);
    }
}
