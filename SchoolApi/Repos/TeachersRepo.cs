using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolWebsite.shared;

namespace SchoolApi.Repos
{
    public class TeachersRepo
    {
        private readonly AppDbContext context;

        public TeachersRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Teacher>> GetTeachers()
            => await context.Teachers.ToListAsync();
        public async Task<Teacher> GetTeacher(string id)
        {
            var Teacher = await context.Teachers.SingleOrDefaultAsync(s => s.Id == id);
            return Teacher;
        }
        public async Task<Teacher> AddTeacher(Teacher teacher)
        {
            var addedTeacher = await context.AddAsync(teacher);
            await context.SaveChangesAsync();
            return addedTeacher.Entity;
        }
        public async Task<Teacher> UpdateTeacher(Teacher teacher, string id)
        {
            var existingTeacher = await context.Teachers.SingleOrDefaultAsync(t => t.Id == id);
            if (existingTeacher == null)
                return null;

            existingTeacher.Name = teacher.Name;
            existingTeacher.Subjects = teacher.Subjects;
            context.SaveChanges();
            return existingTeacher;
        }

        public async Task<Teacher> DeleteTeacher(string teacherId)
        {
            var teacherToDelete = await GetTeacher(teacherId);

            if (teacherToDelete != null)
            {
                context.Teachers.Remove(teacherToDelete);
                await context.SaveChangesAsync();
                return teacherToDelete;
            }

            return null;
        }
    }
}

