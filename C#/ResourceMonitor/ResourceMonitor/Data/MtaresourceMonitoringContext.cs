using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ResourceMonitor.Data;

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=mtadev;Database=MTAResourceMonitoring;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Process__3214EC278479F0CF");

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
            entity.HasKey(e => e.Id).HasName("PK__Server__3214EC27C89EE375");

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
            entity.HasKey(e => e.Id).HasName("PK__Service__3214EC27775ACB52");

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
            entity.HasKey(e => e.Id).HasName("PK__Website__3214EC278DD6EA1A");

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
