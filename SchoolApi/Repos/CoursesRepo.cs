using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolWebsite.shared;

namespace SchoolApi.Repos
{
    public class CoursesRepo
    {
        private readonly AppDbContext context;

        public CoursesRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Course>> GetCourses(int colId)
        {
            return await context.Courses
                .Where(c => c.CollegeId == colId)
                .Include(c=>c.College)
                .Include(c=>c.Students)
                .ToListAsync();
        }
        public async Task<Course> GetCourse (int courseId)
        {
            var course = await context.Courses
                .Include(c => c.College)
                .Include(c => c.Students)
                .Include(c=>c.Teachers)
                .SingleOrDefaultAsync(s => s.Id == courseId);
            return course;
        }
        public async Task<Course> AddCourse (int collegeId,Course course)
        {
            var college = context.Colleges.Find(collegeId);
            if (college is not null)
            {
                Course addedSubject = (await context.AddAsync(course)).Entity;
                addedSubject.CollegeId = collegeId;
                addedSubject.College = college;
                await context.SaveChangesAsync();
                return addedSubject;
            }
            return null;
        }
        public async Task<Course> UpdateCourse(Course editedCourse, int id)
        {
            var existingSubject = await context.Courses.SingleOrDefaultAsync(s => s.Id == id);
            if (existingSubject == null)
                return null;

            context.Entry(existingSubject).CurrentValues.SetValues(editedCourse);
            await context.SaveChangesAsync();
            return existingSubject;
        }

        public async Task<Course> DeleteCourse(int subjectId)
        {
            var movieToDelete = await GetCourse(subjectId);

            if (movieToDelete != null)
            {
                context.Courses.Remove(movieToDelete);
                await context.SaveChangesAsync();
                return movieToDelete;
            }
            return null;
        }
    }
}
