using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolWebsite.shared.Models;

namespace SchoolApi.Repos;

public class CoursesRepo:Repository<Course>
{
    public CoursesRepo(AppDbContext context) : base(context) { }
}
