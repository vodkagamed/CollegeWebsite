using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SchoolApi.Data;
using SchoolWebsite.shared.Models;

namespace SchoolApi.Repos;

public class StudentsRepo:Repository<Student>
{
    public StudentsRepo(AppDbContext context) : base(context) { }
}
