using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolWebsite.shared;

namespace SchoolApi.Configurations
{
    public class TeacherConfig : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(T => T.Subject)
                .WithMany(s => s.Teachers)
                .HasForeignKey(T=>T.SubjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.HasOne(T => T.College)
                .WithMany(C=>C.Teachers)
                .HasForeignKey(T => T.CollegeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
        }
    }
}
