using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using uniga_internship_project.Data;
using uniga_internship_project.Services.Criteria;

namespace uniga_internship_project.Services.SkillSevice
{
    public class SkillService : ISkillService
    {
        private readonly DataContext _dataContext;

        public SkillService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> create(string name)
        {
            var newskill = new Skill()
            {
                Name = name,
                IsActive = true,
            };
            await _dataContext.Skill.AddAsync(newskill); 
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Skill> Get(int Id)
        {
            var skill = await _dataContext.Skill.FindAsync(Id);
            if (skill == null)
            {
                throw new Exception("Data Notfound!");
            }
            return skill;
        }

        public async Task<List<Skill>> Search(SearchSkillCriteria request)
        {
            var query = _dataContext.Skill.AsQueryable();
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
    }
}
