using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.shared;

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
        }
    }
}
