using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MTAIntranetAngular.API.Data.Models;

namespace MTAIntranetAngular.API;

public partial class MtaticketsContext : DbContext
{
    public MtaticketsContext()
    {
    }

    public MtaticketsContext(DbContextOptions<MtaticketsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApprovalState> ApprovalStates { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Impact> Impacts { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketSubType> TicketSubTypes { get; set; }
    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            "Server=mtadev;database=MTATickets;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApprovalStateDTO>(entity =>
        {
            entity.HasKey(e => e.ApprovalStateId).HasName("PK__Approval__BE49849A9AA9BEE2");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B1132D416");
        });

        modelBuilder.Entity<Impact>(entity =>
        {
            entity.HasKey(e => e.ImpactId).HasName("PK__Impact__2297C5DDC4C1F5C0");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC62771B1B6BF");

            entity.Property(e => e.ApprovedBy).HasDefaultValueSql("('NA')");
            entity.Property(e => e.ReasonForRejection).HasDefaultValueSql("('NA')");

            entity.HasOne(d => d.ApprovalState).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__Approval__3019FEA4");

            entity.HasOne(d => d.Category).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__Category__2C496DC0");

            entity.HasOne(d => d.Impact).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__ImpactID__2E31B632");

            entity.HasOne(d => d.SubType).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__SubTypeI__2D3D91F9");
        });

        modelBuilder.Entity<TicketSubType>(entity =>
        {
            entity.HasKey(e => e.TicketSubTypeId).HasName("PK__TicketSu__D4EA10617164F171");

            entity.HasOne(d => d.Category).WithMany(p => p.TicketSubTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TicketSub__Categ__259C7031");
        });

        //base.OnModelCreating(modelBuilder); // ADDED

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
