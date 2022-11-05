using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JobPortal2.Models.DBObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JobPortal2.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; } = null!;
        /*public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;*/
        public virtual DbSet<Candidate> Candidates { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Recruiter> Recruiters { get; set; } = null!;
        public virtual DbSet<Saved> Saveds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=JobPortal2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.IdApplication)
                    .HasName("PK__Applicat__C67BBB1FFFCDCEDD");

                entity.ToTable("Application");

                entity.Property(e => e.IdApplication).ValueGeneratedNever();

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.HasOne(d => d.IdCandidateNavigation)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.IdCandidate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Application_Candidate");

                entity.HasOne(d => d.IdJobNavigation)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.IdJob)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Application_Job");
            });

            /*modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });*/

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.IdCandidate)
                    .HasName("PK__Candidat__D5598973B00EA55F");

                entity.ToTable("Candidate");

                entity.Property(e => e.IdCandidate).ValueGeneratedNever();

                entity.Property(e => e.Experience)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Resume).IsUnicode(false);

                entity.Property(e => e.Skills)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.IdJob)
                    .HasName("PK__Job__0CD8DCD7A1C92EDD");

                entity.ToTable("Job");

                entity.Property(e => e.IdJob).ValueGeneratedNever();

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRecruiterNavigation)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.IdRecruiter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Job_Recruiter");
            });

            modelBuilder.Entity<Recruiter>(entity =>
            {
                entity.HasKey(e => e.IdRecruiter)
                    .HasName("PK__Recruite__75EF1C7C2C9AE532");

                entity.ToTable("Recruiter");

                entity.Property(e => e.IdRecruiter).ValueGeneratedNever();

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Saved>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Saved");

                entity.HasOne(d => d.IdCandidateNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCandidate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Saved_Candidate");

                entity.HasOne(d => d.IdJobNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdJob)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Saved_Job");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
