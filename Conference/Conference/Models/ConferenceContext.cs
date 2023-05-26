using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Conference.Models;

public partial class ConferenceContext : DbContext
{
    public ConferenceContext()
    {
    }

    public ConferenceContext(DbContextOptions<ConferenceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ConnectionLocalDb"].ToString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__City__3214EC071C3A7B53");

            entity.ToTable("City");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC07A179F75B");

            entity.ToTable("Country");

            entity.Property(e => e.Code).HasMaxLength(2);
            entity.Property(e => e.EnglishName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3214EC0723449591");

            entity.ToTable("Event");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Direction).HasMaxLength(100);
            entity.Property(e => e.Photo).IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.Events)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__CityId__2A4B4B5E");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Particip__3214EC0728C51A36");

            entity.ToTable("Participant");

            entity.Property(e => e.Birthdate).HasColumnType("date");
            entity.Property(e => e.Direction).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(1);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Patronymic).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.Participants)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__Count__2D27B809");

            entity.HasOne(d => d.Role).WithMany(p => p.Participants)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__RoleI__2E1BDC42");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07618B46E8");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
