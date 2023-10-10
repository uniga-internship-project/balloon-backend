using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using uniga_internship_project.Data;
using uniga_internship_project.Services.Criteria;

namespace uniga_internship_project.Services.PlansService
{
    public class PlansService : IPlansService
    {
        private readonly DataContext _dataContext;

        public PlansService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> create(string name, string description)
        {
            var newPlans = new Plan()
            {
                Name = name,
                Description = description,
                IsActive = true,
            };
            await _dataContext.Plan.AddAsync(newPlans);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> delete(bool IsActive)
        {
            var planToDelete = await _dataContext.Plan.FirstOrDefaultAsync(p => p.IsActive == IsActive);

            if (planToDelete != null)
            {
                _dataContext.Plan.Remove(planToDelete);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            else 
            { 
                return false;
            }
        }
        public async Task<Plan> Get(int Id)
        {
            var plan = await _dataContext.Plan.FindAsync(Id);
            if (plan == null)
            {
                throw new Exception("Data Notfound!");
            }
            return plan;
        }
        public async Task<List<Plan>> Search(SearchPlansCriteria request)
        {
            var query = _dataContext.Plan.AsQueryable();
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(item => item.Name.Contains(request.Name));
            }
            var count = await query.CountAsync();
            var page = (int)Math.Ceiling((decimal)count / request.PageSize);
            var itemPerPage = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            return itemPerPage; 
        }
        public async Task<Plan> Edit (string name , string description)
        {
            var editplan = await _dataContext.Plan.FindAsync (name, description);
            if (editplan == null)
            {
                throw new Exception("Data Notfound!");
            }
            await _dataContext.Plan.AddAsync(editplan);
            await _dataContext.SaveChangesAsync();
            return editplan;
        }
        public async Task<bool> ActiveEdit(string name, string description, bool IsActive)
        {
            var activeedit = await _dataContext.Plan.FindAsync(name, description);
            if (activeedit == null)
            {
                throw new Exception("Data Notfound!");
            }
            activeedit.IsActive = IsActive;

            await _dataContext.SaveChangesAsync();
            await _dataContext.Plan.AddAsync(activeedit);
            await _dataContext.SaveChangesAsync();
            return activeedit.IsActive;
        }
    }
}