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
            => await context.Teachers.Include(x => x.Course).ToListAsync();
        public async Task<Teacher> GetTeacher(int id)
        {
            var Teacher = await context.Teachers
                .Include(T=>T.College)
                .Include(T=>T.Course)
                .SingleOrDefaultAsync(s => s.Id == id);
            return Teacher;
        }
        public async Task<Teacher> AddTeacher(int collegeId, Teacher teacher)
        {
            College availableCollege = await context.Colleges.FindAsync(collegeId);
            if (availableCollege is not null)
            {
                var addedTeacher = (await context.AddAsync(teacher)).Entity;
                addedTeacher.College = availableCollege;
                addedTeacher.CollegeId = collegeId;
                await context.SaveChangesAsync();
                return addedTeacher;
            }
            return null;
        }
        public async Task<Teacher> UpdateTeacher(Teacher editedTeacher, int id)
        {
            var existingTeacher = await context.Teachers.SingleOrDefaultAsync(t => t.Id == id);
            if (existingTeacher == null)
                return null;

           context.Entry(existingTeacher).CurrentValues.SetValues(editedTeacher);
            context.SaveChanges();
            return existingTeacher;
        }

        public async Task<Teacher> DeleteTeacher(int teacherId)
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

