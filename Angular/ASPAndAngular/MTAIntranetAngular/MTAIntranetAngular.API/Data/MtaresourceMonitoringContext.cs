using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MTAIntranetAngular.API.Data.Models;

namespace MTAIntranetAngular.API;

public partial class MtaresourceMonitoringContext : DbContext
{
    public MtaresourceMonitoringContext()
    {
    }

    public MtaresourceMonitoringContext(DbContextOptions<MtaresourceMonitoringContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Process> Processes { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Website> Websites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=mtadev;Database=MTAResourceMonitoring;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Process__3214EC27973F7EC1");

            entity.ToTable("Process");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CurrentState)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastCheck).HasColumnType("datetime");
            entity.Property(e => e.LastEmailsent).HasColumnType("datetime");
            entity.Property(e => e.PreviousState)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ProcessName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Recipients)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ServerName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Server__3214EC272BF546B8");

            entity.ToTable("Server");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CurrentState)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastCheck).HasColumnType("datetime");
            entity.Property(e => e.LastEmailsent).HasColumnType("datetime");
            entity.Property(e => e.PreviousState)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Recipients)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ServerName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Service__3214EC274CCAC818");

            entity.ToTable("Service");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CurrentState)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastCheck).HasColumnType("datetime");
            entity.Property(e => e.LastEmailsent).HasColumnType("datetime");
            entity.Property(e => e.PreviousState)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Recipients)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ServerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Website>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Website__3214EC27CB0029B6");

            entity.ToTable("Website");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CurrentState)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastCheck).HasColumnType("datetime");
            entity.Property(e => e.LastEmailsent).HasColumnType("datetime");
            entity.Property(e => e.PreviousState)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Recipients)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ServerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WebsiteName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
