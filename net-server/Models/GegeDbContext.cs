using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace net_server.Models;

public partial class GegeDbContext : DbContext
{
    public GegeDbContext()
    {
    }

    public GegeDbContext(DbContextOptions<GegeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Jobdetail> Jobdetails { get; set; }

    public virtual DbSet<Joblisting> Joblistings { get; set; }

    public virtual DbSet<New> News { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productattribute> Productattributes { get; set; }

    public virtual DbSet<Productimage> Productimages { get; set; }

    public virtual DbSet<ProductsCategory> ProductsCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;uid=root;pwd=123456;database=gege_db", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.AboutId).HasName("PRIMARY");

            entity.ToTable("about");

            entity.Property(e => e.CompanyIntroduction).HasColumnType("text");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("banner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ProductImage)
                .HasMaxLength(255)
                .HasColumnName("product_image");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.Subtitle)
                .HasMaxLength(255)
                .HasColumnName("subtitle");
        });

        modelBuilder.Entity<Jobdetail>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PRIMARY");

            entity.ToTable("jobdetails");

            entity.Property(e => e.JobTitle).HasMaxLength(255);
            entity.Property(e => e.Qualifications).HasColumnType("text");
            entity.Property(e => e.Responsibilities).HasColumnType("text");
            entity.Property(e => e.Salary).HasMaxLength(255);
        });

        modelBuilder.Entity<Joblisting>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PRIMARY");

            entity.ToTable("joblistings");

            entity.Property(e => e.Department).HasMaxLength(255);
            entity.Property(e => e.JobTitle).HasMaxLength(255);
        });

        modelBuilder.Entity<New>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("new");

            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.Cover)
                .HasMaxLength(255)
                .HasColumnName("cover");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("products");

            entity.HasIndex(e => e.CategoryId, "CategoryId");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ProductImage).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.Subtitle).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_ibfk_1");
        });

        modelBuilder.Entity<Productattribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PRIMARY");

            entity.ToTable("productattributes");

            entity.HasIndex(e => e.ProductId, "ProductId");

            entity.Property(e => e.AttributeName).HasMaxLength(255);
            entity.Property(e => e.AttributeValue).HasMaxLength(255);

            entity.HasOne(d => d.Product).WithMany(p => p.Productattributes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productattributes_ibfk_1");
        });

        modelBuilder.Entity<Productimage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PRIMARY");

            entity.ToTable("productimages");

            entity.HasIndex(e => e.ProductId, "ProductId");

            entity.Property(e => e.ImageUrl).HasMaxLength(255);

            entity.HasOne(d => d.Product).WithMany(p => p.Productimages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productimages_ibfk_1");
        });

        modelBuilder.Entity<ProductsCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("products_categories");

            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
