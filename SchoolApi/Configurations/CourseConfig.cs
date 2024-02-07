using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.shared.Models;

namespace SchoolApi.Configurations;

public class CourseConfig : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasMany(C => C.Teachers)
               .WithOne()
               .HasForeignKey(t => t.CourseId)
               .OnDelete(deleteBehavior:DeleteBehavior.NoAction)
               .IsRequired();

        builder.HasIndex(c => new { c.CollegeId, c.Name }).IsUnique();
    }
}
