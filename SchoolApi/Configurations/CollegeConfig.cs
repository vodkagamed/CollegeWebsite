using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.shared;

namespace SchoolApi.Configurations
{
    public class CollegeConfig : IEntityTypeConfiguration<College>
    {
        public void Configure(EntityTypeBuilder<College> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Students)
                .WithOne(s => s.College)
                .HasForeignKey(s => s.CollegeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.HasMany(c => c.Courses)
                .WithOne(sub => sub.College)
                .HasForeignKey(sub => sub.CollegeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.HasMany(c => c.Teachers)
                .WithOne(t => t.College)
                .HasForeignKey(t => t.CollegeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
