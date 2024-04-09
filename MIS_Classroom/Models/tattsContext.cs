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
                entity.HasKey(e => e.UselD)
                    .HasName("PK__techengi__64B82F982161D48E");

                entity.ToTable("techenginee_MIS_Credentials");

                entity.Property(e => e.UselD).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TechengineeMisQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionID); // Set the primary key to QuestionID

                entity.ToTable("techenginee_MIS_Question");

                entity.Property(e => e.QuestionsTxt).IsUnicode(false);

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TechengineeMisStudent>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("PK__techengi__32C52A79BB5A1437");

                entity.ToTable("techenginee_MIS_Student");

                entity.Property(e => e.StudentId)
                    .ValueGeneratedNever()
                    .HasColumnName("StudentID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TechengineeMisSubject>(entity =>
            {
                entity.HasKey(e => e.SubjectCode);

                entity.ToTable("techenginee_MIS_Subject");

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SubjectName).HasMaxLength(50);
            });

            modelBuilder.Entity<TechengineeMisTeacher>(entity =>
            {
                entity.HasKey(e => e.TeacherId)
                    .HasName("PK__techengi__EDF25944BFB0952E");

                entity.ToTable("techenginee_MIS_Teacher");

                entity.Property(e => e.TeacherId)
                    .ValueGeneratedNever()
                    .HasColumnName("TeacherID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Subjectcode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TechengineeMisUserType>(entity =>
            {
                entity.HasKey(e => e.UserType)
                    .HasName("PK__techengi__87E786902EBBC866");

                entity.ToTable("techenginee_MIS_UserType");

                entity.Property(e => e.UserType)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
