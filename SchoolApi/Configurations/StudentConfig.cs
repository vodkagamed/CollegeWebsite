using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using SchoolWebsite.shared;

namespace SchoolApi.Configurations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(S => S.Subjects)
                .WithMany(J => J.Students);
        }
    }
}
