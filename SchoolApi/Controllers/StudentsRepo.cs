using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolWebsite.shared;

namespace SchoolApi.Controllers
{
    public class StudentsRepo
    {
        private readonly AppDbContext context;

        public StudentsRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Student>> GetStudents()
            => await context.Students.ToListAsync();
        public async Task<Student> GetStudent(int id)
        {
            var student = await context.Students.SingleOrDefaultAsync(s => s.Id == id);
            return student;
        }
        public async Task<Student> AddStudent(Student student)
        {
            var addedStudent = await context.AddAsync(student);
            await context.SaveChangesAsync();
            return addedStudent.Entity;
        }
        public async Task<Student> UpdateStudent(Student student,int id)
        {
            var existingStudent = await context.Students.SingleOrDefaultAsync(s => s.Id == id);
            if (existingStudent == null)
                return null;

            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Phone = student.Phone;
            context.SaveChanges();
            return existingStudent;
        }

        public async Task<Student> DeleteStudent(int studentId)
        {
            var movieToDelete = await GetStudent(studentId);

            if (movieToDelete != null)
            {
                context.Students.Remove(movieToDelete);
                await context.SaveChangesAsync();
                return movieToDelete;
            }

            return null;
        }
    }
}
