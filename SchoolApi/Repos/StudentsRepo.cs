using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolWebsite.shared;

namespace SchoolApi.Repos
{
    public class StudentsRepo
    {
        private readonly AppDbContext context;

        public StudentsRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Student>> GetStudents()
            => await context.Students
            .Include(S => S.Courses)
            .Include(S => S.College).ToListAsync();
        public async Task<Student> GetStudent(int id)
        {
            var student = await context.Students
                .Include(S => S.Courses)
                .Include(S => S.College)
                .SingleOrDefaultAsync(s => s.Id == id);
            return student;
        }
        public async Task<Student> AddStudent(int collegeId, Student student)
        {

            College availableCollege = await context.Colleges.FindAsync(collegeId);
            if (availableCollege is not null)
            {
                var addedStudent = (await context.AddAsync(student)).Entity;
                addedStudent.CollegeId = collegeId;
                addedStudent.College = availableCollege;
                await context.SaveChangesAsync();
                return addedStudent;
            }
            return null;
        }
        public async Task<Student> UpdateStudent(Student editedStudent, int id)
        {
            var existingStudent = await context.Students.SingleOrDefaultAsync(s => s.Id == id);
            if (existingStudent == null)
                return null;

            context.Entry(existingStudent).CurrentValues.SetValues(editedStudent);
            context.SaveChanges();
            return existingStudent;
        }

        public async Task<Student> DeleteStudent(int studentId)
        {
            var studentToDelete = await GetStudent(studentId);

            if (studentToDelete != null)
            {
                context.Students.Remove(studentToDelete);
                await context.SaveChangesAsync();
                return studentToDelete;
            }

            return null;
        }

        public async Task<Course> EnrollCourse(int studentId, int courseId)
        {
            var student = await context.Students.FindAsync(studentId);
            var course = await context.Courses.FindAsync(courseId);
            if (student != null && course != null)
            {
                student.Courses.Add(course);
                await context.SaveChangesAsync();
                return course;
            }
            return null;
        }
        public async Task<Course> CancelCourse(int studentId, int courseId)
        {
            var student = await context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == studentId);
            var course = await context.Courses.FindAsync(courseId);
            if (student != null && course != null)
            {
                student.Courses.Remove(course);
                await context.SaveChangesAsync();
                return course;
            }
            return null;
        }

    }
}
