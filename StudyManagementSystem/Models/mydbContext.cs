﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BachelorBackEnd
{
    public partial class mydbContext : DbContext
    {
        public mydbContext()
        {
        }

        public mydbContext(DbContextOptions<mydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Participants> Participants { get; set; }
        public virtual DbSet<TestTable> TestTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=DESKTOP-4G1FLIU;Database=mydb;user=capper;pwd=1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participants>(entity =>
            {
                entity.HasKey(e => e.IdParticipant);

                entity.ToTable("participants");

                entity.Property(e => e.IdParticipant)
                    .HasColumnName("id_participant")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Pause)
                    .HasColumnName("pause")
                    .HasColumnType("tinyint(4)");
            });

            modelBuilder.Entity<TestTable>(entity =>
            {
                entity.ToTable("test_table");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Age)
                    .HasColumnName("age")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Smoker)
                    .HasColumnName("smoker")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime");
            });
        }
    }
}
