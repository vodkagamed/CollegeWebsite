using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolWebsite.shared;
using SchoolWebsite.shared.Models;

namespace SchoolApi.Repos
{
    public class CollegesRepo
    {
        private readonly AppDbContext context;
        //private readonly DataProtector protector;   
        public CollegesRepo(AppDbContext context, DataProtector protector)
        {
            this.context = context;
            //this.protector = protector;
        }
        public async Task<IEnumerable<College>> GetColleges()
            => await context.Colleges
            .Include(c=>c.Teachers)
            .Include(c=>c.Courses)
            .Include(c=>c.Students)
            .ToListAsync();
        public async Task<College> GetCollege(int id)
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
        public async Task<College> UpdateCollege(College editedCollege, int id)
        {
            var existingCollege = await context.Colleges.SingleOrDefaultAsync(s => s.Id == id);
            if (existingCollege == null)
                return null;

            context.Entry(existingCollege).CurrentValues.SetValues(editedCollege);
            await context.SaveChangesAsync();
            return existingCollege;
        }

        public async Task<College> DeleteCollege(int collegeId)
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

