using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BachelorBackEnd
{
    public partial class bachelordbContext : DbContext
    {
        public bachelordbContext()
        {
        }

        public bachelordbContext(DbContextOptions<bachelordbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inclusioncriteria> Inclusioncriteria { get; set; }
        public virtual DbSet<Participant> Participant { get; set; }
        public virtual DbSet<Researcher> Researcher { get; set; }
        public virtual DbSet<Study> Study { get; set; }
        public virtual DbSet<Studyparticipant> Studyparticipant { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=10.192.79.2;Database=bachelordb;user=admin;pwd=admin1234;Convert Zero Datetime = true");
              //  DESKTOP - 4G1FLIU
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inclusioncriteria>(entity =>
            {
                entity.HasKey(e => e.IdInclusionCriteria);

                entity.ToTable("inclusioncriteria");

                entity.HasIndex(e => e.IdInclusionCriteria)
                    .HasName("id_inclusionCriteria_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdStudy)
                    .HasName("id_study_idx");

                entity.Property(e => e.IdInclusionCriteria)
                    .HasColumnName("id_inclusionCriteria")
                    .HasColumnType("int(11)");

                entity.Property(e => e.English)
                    .HasColumnName("english")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Female)
                    .HasColumnName("female")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.IdStudy)
                    .HasColumnName("id_study")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Male)
                    .HasColumnName("male")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.MaxAge)
                    .HasColumnName("max_age")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MinAge)
                    .HasColumnName("min_age")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Inclusioncriteria)
                    .HasForeignKey(d => d.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_study1");
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasKey(e => e.IdParticipant);

                entity.ToTable("participant");

                entity.HasIndex(e => e.IdParticipant)
                    .HasName("idParticipant_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdParticipant)
                    .HasColumnName("id_participant")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Age)
                    .HasColumnName("age")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.English)
                    .HasColumnName("english")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Researcher>(entity =>
            {
                entity.HasKey(e => e.IdResearcher);

                entity.ToTable("researcher");

                entity.HasIndex(e => e.IdResearcher)
                    .HasName("id_researcher_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdResearcher)
                    .HasColumnName("id_researcher")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Isadmin)
                    .HasColumnName("isadmin")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Isverified)
                    .HasColumnName("isverified")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Study>(entity =>
            {
                entity.HasKey(e => e.IdStudy);

                entity.ToTable("study");

                entity.HasIndex(e => e.IdResearcher)
                    .HasName("id_researcher_idx");

                entity.HasIndex(e => e.IdStudy)
                    .HasName("id_study_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdStudy)
                    .HasColumnName("id_study")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IdResearcher)
                    .HasColumnName("id_researcher")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Isdraft)
                    .HasColumnName("isdraft")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasColumnName("tag")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdResearcherNavigation)
                    .WithMany(p => p.Study)
                    .HasForeignKey(d => d.IdResearcher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_researcher");
            });

            modelBuilder.Entity<Studyparticipant>(entity =>
            {
                entity.HasKey(e => e.IdStudyParticipant);

                entity.ToTable("studyparticipant");

                entity.HasIndex(e => e.IdParticipant)
                    .HasName("id_participantstudy_idx");

                entity.HasIndex(e => e.IdStudy)
                    .HasName("id_study_idx");

                entity.HasIndex(e => e.IdStudyParticipant)
                    .HasName("id_studyParticipant_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdStudyParticipant)
                    .HasColumnName("id_studyParticipant")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdParticipant)
                    .HasColumnName("id_participant")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdStudy)
                    .HasColumnName("id_study")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdParticipantNavigation)
                    .WithMany(p => p.Studyparticipant)
                    .HasForeignKey(d => d.IdParticipant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_participant1");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Studyparticipant)
                    .HasForeignKey(d => d.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_study");
            });
        }
    }
}
