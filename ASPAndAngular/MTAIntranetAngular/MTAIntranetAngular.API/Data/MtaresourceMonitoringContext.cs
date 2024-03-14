using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MTAIntranetAngular.API.Data.Models;

namespace MTAIntranetAngular.API.Data;

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
        => optionsBuilder.UseSqlServer("Server=mtadev;Database=MTAResourceMonitoring;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Process__3214EC27E8E02B8E");

            entity.ToTable("Process");

            entity.Property(e => e.Id).HasColumnName("ID");
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
            entity.HasKey(e => e.Id).HasName("PK__Server__3214EC273222FDCD");

            entity.ToTable("Server");

            entity.Property(e => e.Id).HasColumnName("ID");
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
            entity.HasKey(e => e.Id).HasName("PK__Service__3214EC275D05BBA5");

            entity.ToTable("Service");

            entity.Property(e => e.Id).HasColumnName("ID");
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
            entity.HasKey(e => e.Id).HasName("PK__Website__3214EC27E0A1942A");

            entity.ToTable("Website");

            entity.Property(e => e.Id).HasColumnName("ID");
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
