﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.shared;

namespace SchoolApi.Configurations
{
    public class CollegeConfig : IEntityTypeConfiguration<College>
    {
        public void Configure(EntityTypeBuilder<College> builder)
        {
            builder.HasKey(c => c.Id);

            // Remove cascade delete for Students, Courses, and Teachers
            builder.HasMany(c => c.Students)
                .WithOne(s => s.College)
                .HasForeignKey(s => s.CollegeId)
                .IsRequired(false);

            builder.HasMany(c => c.Courses)
                .WithOne(sub => sub.College)
                .HasForeignKey(sub => sub.CollegeId)
                .IsRequired(false);

            builder.HasMany(c => c.Teachers)
                .WithOne(t => t.College)
                .HasForeignKey(t => t.CollegeId)
                .IsRequired(false);
        }
    }

}
