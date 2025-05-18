using System;
using System.Collections.Generic;
using Cineplex_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Cineplex_Management.Models;

public partial class CineplexContext : DbContext
{
    public CineplexContext()
    {
    }

    public CineplexContext(DbContextOptions<CineplexContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hall> Halls { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalesDetail> SalesDetails { get; set; }

    public virtual DbSet<SeatPlan> SeatPlans { get; set; }

    public virtual DbSet<Show> Shows { get; set; }

    public virtual DbSet<ShowDetail> ShowDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hall>(entity =>
        {
            entity.HasKey(e => e.HallId).HasName("PK__HALL__7E60E214602EBDF7");

            entity.ToTable("HALL");

            entity.Property(e => e.HallName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__4BD2941AB3599871");

            entity.ToTable("Movie");

            entity.Property(e => e.MovieName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38A8B29A4E");

            entity.ToTable("Payment");

            entity.Property(e => e.AccNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BankName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentTypeId)
                .HasConstraintName("FK__Payment__Payment__5629CD9C");

            entity.HasOne(d => d.Sales).WithMany(p => p.Payments)
                .HasForeignKey(d => d.SalesId)
                .HasConstraintName("FK__Payment__SalesId__5535A963");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PaymentTypeId).HasName("PK__paymentT__BA430B35F7254E4C");

            entity.ToTable("paymentType");

            entity.Property(e => e.PaymentTypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AADCCF4E1");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SalesId).HasName("PK__Sale__C952FB32D7424E5C");

            entity.ToTable("Sale");

            entity.Property(e => e.Time).HasColumnType("datetime");
            entity.Property(e => e.TotalPay).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<SalesDetail>(entity =>
        {
            entity.HasKey(e => e.SalesDetailsId).HasName("PK__SalesDet__FE1B9AC55CAE1545");

            entity.HasOne(d => d.Sales).WithMany(p => p.SalesDetails)
                .HasForeignKey(d => d.SalesId)
                .HasConstraintName("FK__SalesDeta__Sales__4CA06362");

            entity.HasOne(d => d.ShowDetails).WithMany(p => p.SalesDetails)
                .HasForeignKey(d => d.ShowDetailsId)
                .HasConstraintName("FK__SalesDeta__ShowD__4D94879B");
        });

        modelBuilder.Entity<SeatPlan>(entity =>
        {
            entity.HasKey(e => e.SeatPlanId).HasName("PK__SeatPlan__9C7E27CA1F9584FC");

            entity.ToTable("SeatPlan");

            entity.Property(e => e.SeatNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Hall).WithMany(p => p.SeatPlans)
                .HasForeignKey(d => d.HallId)
                .HasConstraintName("FK__SeatPlan__HallId__5070F446");
        });

        modelBuilder.Entity<Show>(entity =>
        {
            entity.HasKey(e => e.ShowId).HasName("PK__Show__6DE3E0B26634086D");

            entity.ToTable("Show");

            entity.Property(e => e.ShowEnd).HasColumnType("datetime");
            entity.Property(e => e.ShowName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ShowStart).HasColumnType("datetime");

            entity.HasOne(d => d.Hall).WithMany(p => p.Shows)
                .HasForeignKey(d => d.HallId)
                .HasConstraintName("FK__Show__HallId__45F365D3");

            entity.HasOne(d => d.Movie).WithMany(p => p.Shows)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__Show__MovieId__44FF419A");
        });

        modelBuilder.Entity<ShowDetail>(entity =>
        {
            entity.HasKey(e => e.ShowDetailsId).HasName("PK__ShowDeta__07FA4CECA16A51B5");

            entity.Property(e => e.SeatNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Hall).WithMany(p => p.ShowDetails)
                .HasForeignKey(d => d.HallId)
                .HasConstraintName("FK__ShowDetai__HallI__49C3F6B7");

            entity.HasOne(d => d.Show).WithMany(p => p.ShowDetails)
                .HasForeignKey(d => d.ShowId)
                .HasConstraintName("FK__ShowDetai__ShowI__48CFD27E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CCCE0B714");

            entity.ToTable("User");

            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A3516B8DD19");

            entity.ToTable("UserRole");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRole__RoleId__3C69FB99");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRole__UserId__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
