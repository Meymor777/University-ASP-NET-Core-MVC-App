using Microsoft.EntityFrameworkCore;
using UniversityWebApp.Models;

namespace UniversityWebApp.Data;

public partial class UniversityDbContext : DbContext
{
    public UniversityDbContext()
    {
    }

    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-QIIEQM9; Initial Catalog=UniversityDB; User Id=TestUser; Password=testPass123; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__COURSES__71CB31DBCE873BC7");

            entity.ToTable("COURSES");

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("COURSE_ID");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__GROUPS__3EFEA3DE50B89559");

            entity.ToTable("GROUPS");

            entity.Property(e => e.GroupId)
                .ValueGeneratedNever()
                .HasColumnName("GROUP_ID");
            entity.Property(e => e.CourseId).HasColumnName("COURSE_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");

            entity.HasOne(d => d.Course).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__GROUPS__COURSE_I__440B1D61");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__STUDENTS__E69FE77B602858DA");

            entity.ToTable("STUDENTS");

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("STUDENT_ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__STUDENTS__GROUP___46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
