using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Workspace.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<HargaMember> HargaMembers { get; set; }

    public virtual DbSet<Kasir> Kasirs { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Penjualan> Penjualans { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductToPenjualan> ProductToPenjualans { get; set; }

    public virtual DbSet<Utang> Utangs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=toko_sinar_terang;user=root;password=admin", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.3.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("admin")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("date_added");
            entity.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasColumnName("last_login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<HargaMember>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("harga_member")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Harga).HasColumnName("harga");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
        });

        modelBuilder.Entity<Kasir>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("kasir")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("'1'")
                .HasColumnName("flag");
            entity.Property(e => e.Kode)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("kode");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("member")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("date_added");
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("'1'")
                .HasColumnName("flag");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Penjualan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("penjualan")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("date_added");
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("'1'")
                .HasColumnName("flag");
            entity.Property(e => e.KasirId).HasColumnName("kasir_id");
            entity.Property(e => e.MemberId)
                .HasDefaultValueSql("'0'")
                .HasColumnName("member_id");
            entity.Property(e => e.Total).HasColumnName("total");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("product")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Barcode)
                .HasMaxLength(255)
                .HasColumnName("barcode");
            entity.Property(e => e.DateAdded)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("date_added");
            entity.Property(e => e.Expired)
                .HasMaxLength(255)
                .HasColumnName("expired");
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("'1'")
                .HasColumnName("flag");
            entity.Property(e => e.Harga).HasColumnName("harga");
            entity.Property(e => e.Modal).HasColumnName("modal");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Satuan)
                .HasMaxLength(100)
                .HasColumnName("satuan");
        });

        modelBuilder.Entity<ProductToPenjualan>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("product_to_penjualan")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Harga).HasColumnName("harga");
            entity.Property(e => e.PenjualanId).HasColumnName("penjualan_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
        });

        modelBuilder.Entity<Utang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("utang")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("date_added");
            entity.Property(e => e.Flag)
                .HasDefaultValueSql("'1'")
                .HasColumnName("flag");
            entity.Property(e => e.Jumlah).HasColumnName("jumlah");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
