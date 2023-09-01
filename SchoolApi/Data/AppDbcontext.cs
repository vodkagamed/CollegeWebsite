﻿using Microsoft.EntityFrameworkCore;
using SchoolWebsite.shared;

namespace SchoolApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<College> Colleges { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var configutation = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var constr = configutation.GetSection("ConnectionString").Value;
            optionsBuilder.UseSqlServer(constr);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(AppDbContext).Assembly);
        }
    }
}
