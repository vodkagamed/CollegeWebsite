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
        public async Task<Subject> GetSubject(string id)
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
        public async Task<Subject> UpdateSubject(Subject subject, string id)
        {
            var existingSubject = await context.Subjects.SingleOrDefaultAsync(s => s.Id == id);
            if (existingSubject == null)
                return null;

            existingSubject.Name = subject.Name;
            context.SaveChanges();
            return existingSubject;
        }

        public async Task<Subject> DeleteSubject(string subjectId)
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
