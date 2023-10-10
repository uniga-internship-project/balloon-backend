using uniga_internship_project.Services.Criteria;

namespace uniga_internship_project.Services.PlansService
{
    public interface IPlansService
    {
        Task<bool> create(string name,string description);
        Task<bool> delete(bool IsActive);
        Task<Plan> Get(int Id);
        Task<List<Plan>> Search(SearchPlansCriteria request);
        Task<Plan> Edit(string name, string description);
        Task<bool> ActiveEdit(string name, string description, bool IsActive);
    }
}