using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MIS_Classroom.Models
{
    public partial class tattsContext : DbContext
    {
        public tattsContext()
        {
        }

        public tattsContext(DbContextOptions<tattsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TechengineeMisCredential> TechengineeMisCredentials { get; set; } = null!;
        public virtual DbSet<TechengineeMisQuestion> TechengineeMisQuestions { get; set; } = null!;
        public virtual DbSet<TechengineeMisStudent> TechengineeMisStudents { get; set; } = null!;
        public virtual DbSet<TechengineeMisSubject> TechengineeMisSubjects { get; set; } = null!;
        public virtual DbSet<TechengineeMisTeacher> TechengineeMisTeachers { get; set; } = null!;
        public virtual DbSet<TechengineeMisUserType> TechengineeMisUserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TechengineeMisCredential>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__techengi__1788CCACB86781B3");

                entity.ToTable("techenginee_MIS_Credentials");

                entity.HasIndex(e => e.Username, "UQ__techengi__536C85E480522E6E")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TechengineeMisCredentials)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__techengin__TypeI__74643BF9");
            });

            modelBuilder.Entity<TechengineeMisQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__techengi__0DC06F8CE6355D0A");

                entity.ToTable("techenginee_MIS_Question");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionsTxt).IsUnicode(false);

                // Configure the relationship with Subject
                entity.HasOne(d => d.Subject)
                    .WithMany()
                    .HasForeignKey(d => d.SubjectCode)
                    .IsRequired();
            });

            modelBuilder.Entity<TechengineeMisStudent>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("PK__techengi__32C52A7919CB96BD");

                entity.ToTable("techenginee_MIS_Student");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TechengineeMisSubject>(entity =>
            {
                entity.HasKey(e => e.SubjectCode)
                    .HasName("PK__techengi__9F7CE1A8C9817DD1");

                entity.ToTable("techenginee_MIS_Subject");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TechengineeMisTeacher>(entity =>
            {
                entity.HasKey(e => e.TeacherId)
                    .HasName("PK__techengi__EDF2594495EC32DA");

                entity.ToTable("techenginee_MIS_Teacher");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                // Configure the relationship with Subject
                entity.HasOne(d => d.Subject)                  // Question has one Subject
                    .WithMany()                                // Subject can have many Questions
                    .HasForeignKey(d => d.SubjectCode)         // Foreign key in Question table
                    .IsRequired();                             // Subject is required for a Question
            });

            modelBuilder.Entity<TechengineeMisUserType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__techengi__516F0395E3F6EEFC");

                entity.ToTable("techenginee_MIS_UserType");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserType)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });



            OnModelCreatingPartial(modelBuilder);
        }



        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
