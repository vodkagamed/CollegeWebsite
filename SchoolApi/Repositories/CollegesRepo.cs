using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using SchoolApi.Data;
using SchoolWebsite.shared;
using SchoolWebsite.shared.Models;

namespace SchoolApi.Repos;

public class CollegesRepo:Repository<College>
{
    public CollegesRepo(AppDbContext context) : base(context) { }
}
