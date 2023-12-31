﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.shared.Models;

namespace SchoolApi.Configurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasMany(C => C.Teachers)
                .WithOne(T => T.Course)
                .HasForeignKey(t => t.CourseId)
                .IsRequired();
            builder.HasIndex(c => new { c.CollegeId, c.Name }).IsUnique();
        }
    }
}
