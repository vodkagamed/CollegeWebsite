using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using SchoolApi.Data;
using SchoolWebsite.shared;
using SchoolWebsite.shared.Models;

namespace SchoolApi.Repos;

public class CollegeRepo:Repository<College>
{
    public CollegeRepo(AppDbContext context) : base(context) { }
}
