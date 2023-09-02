﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolApi.Data;

#nullable disable

namespace SchoolApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("CoursesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("SchoolWebsite.shared.College", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colleges");
                });

            modelBuilder.Entity("SchoolWebsite.shared.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CollegeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CollegeId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SchoolWebsite.shared.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CollegeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CollegeId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SchoolWebsite.shared.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CollegeId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CollegeId");

                    b.HasIndex("CourseId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("SchoolWebsite.shared.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolWebsite.shared.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolWebsite.shared.Course", b =>
                {
                    b.HasOne("SchoolWebsite.shared.College", "College")
                        .WithMany("Courses")
                        .HasForeignKey("CollegeId");

                    b.Navigation("College");
                });

            modelBuilder.Entity("SchoolWebsite.shared.Student", b =>
                {
                    b.HasOne("SchoolWebsite.shared.College", "College")
                        .WithMany("Students")
                        .HasForeignKey("CollegeId");

                    b.Navigation("College");
                });

            modelBuilder.Entity("SchoolWebsite.shared.Teacher", b =>
                {
                    b.HasOne("SchoolWebsite.shared.College", "College")
                        .WithMany("Teachers")
                        .HasForeignKey("CollegeId");

                    b.HasOne("SchoolWebsite.shared.Course", "Course")
                        .WithMany("Teachers")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("College");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("SchoolWebsite.shared.College", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("SchoolWebsite.shared.Course", b =>
                {
                    b.Navigation("Teachers");
                });
#pragma warning restore 612, 618
        }
    }
}
