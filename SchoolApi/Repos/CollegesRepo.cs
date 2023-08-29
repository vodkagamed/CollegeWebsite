using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolWebsite.shared;

namespace SchoolApi.Repos
{
    public class CollegesRepo
    {
        private readonly AppDbContext context;

        public CollegesRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<College>> GetColleges()
            => await context.Colleges.ToListAsync();
        public async Task<College> GetCollege(string id)
        {
            var college = await context.Colleges.SingleOrDefaultAsync(c => c.Id == id);
            return college;
        }
        public async Task<College> AddCollege(College college)
        {
            var addedCollege = await context.Colleges.AddAsync(college);
            await context.SaveChangesAsync();
            return addedCollege.Entity;
        }
        public async Task<College> UpdateCollege(College college, string id)
        {
            var existingCollege = await context.Colleges.SingleOrDefaultAsync(s => s.Id == id);
            if (existingCollege == null)
                return null;

            existingCollege.Name = college.Name;
            await context.SaveChangesAsync();
            return existingCollege;
        }

        public async Task<College> DeleteCollege(string collegeId)
        {
            var collegeToDelete = await GetCollege(collegeId);

            if (collegeToDelete != null)
            {
                context.Colleges.Remove(collegeToDelete);
                await context.SaveChangesAsync();
                return collegeToDelete;
            }

            return null;
        }
    }
}

