﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityWebApp.Data;


#nullable disable

namespace UniversityWebApp.Migrations
{
    [DbContext(typeof(UniversityDbContext))]
    partial class UniversityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UniversityWebApp.Models.Course", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("COURSE_ID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("NAME");

                    b.HasKey("CourseId")
                        .HasName("PK__COURSES__71CB31DBCE873BC7");

                    b.ToTable("COURSES", (string)null);
                });

            modelBuilder.Entity("UniversityWebApp.Models.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("GROUP_ID");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("COURSE_ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NAME");

                    b.HasKey("GroupId")
                        .HasName("PK__GROUPS__3EFEA3DE50B89559");

                    b.HasIndex("CourseId");

                    b.ToTable("GROUPS", (string)null);
                });

            modelBuilder.Entity("UniversityWebApp.Models.Student", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("STUDENT_ID");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("FIRST_NAME");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("GROUP_ID");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("LAST_NAME");

                    b.HasKey("StudentId")
                        .HasName("PK__STUDENTS__E69FE77B602858DA");

                    b.HasIndex("GroupId");

                    b.ToTable("STUDENTS", (string)null);
                });

            modelBuilder.Entity("UniversityWebApp.Models.Group", b =>
                {
                    b.HasOne("UniversityWebApp.Models.Course", "Course")
                        .WithMany("Groups")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__GROUPS__COURSE_I__440B1D61");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("UniversityWebApp.Models.Student", b =>
                {
                    b.HasOne("UniversityWebApp.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__STUDENTS__GROUP___46E78A0C");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("UniversityWebApp.Models.Course", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("UniversityWebApp.Models.Group", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
