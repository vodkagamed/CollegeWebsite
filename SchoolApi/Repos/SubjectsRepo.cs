using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolWebsite.shared;

namespace SchoolApi.Repos
{
    public class SubjectsRepo
    {
        private readonly AppDbContext context;

        public SubjectsRepo(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Subject>> GetSubjects()
            => await context.Subjects.ToListAsync();
        public async Task<Subject> GetSubject(int id)
        {
            var subject = await context.Subjects.SingleOrDefaultAsync(s => s.Id == id);
            return subject;
        }
        public async Task<Subject> AddSubject(Subject subject)
        {
            var addedSubject = await context.AddAsync(subject);
            await context.SaveChangesAsync();
            return addedSubject.Entity;
        }
        public async Task<Subject> UpdateSubject(Subject editedSubject, int id)
        {
            var existingSubject = await context.Subjects.SingleOrDefaultAsync(s => s.Id == id);
            if (existingSubject == null)
                return null;

            context.Entry(existingSubject).CurrentValues.SetValues(editedSubject);
            await context.SaveChangesAsync();
            return existingSubject;
        }

        public async Task<Subject> DeleteSubject(int subjectId)
        {
            var movieToDelete = await GetSubject(subjectId);

            if (movieToDelete != null)
            {
                context.Subjects.Remove(movieToDelete);
                await context.SaveChangesAsync();
                return movieToDelete;
            }

            return null;
        }
    }
}
