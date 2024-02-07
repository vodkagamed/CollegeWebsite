using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolWebsite.shared.Models;

namespace SchoolApi.Repos
{
    public class TeachersRepo:Repository<Teacher>
    {
        public TeachersRepo(AppDbContext context) : base(context) { }
    }
}

