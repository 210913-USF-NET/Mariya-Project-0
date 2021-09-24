using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DL.Entities
{
    public partial class Project00Context : DbContext
    {
        public Project00Context()
        {
        }

        public Project00Context(DbContextOptions<Project00Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<StoreFront> StoreFronts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.CustomerName).IsRequired();

                entity.Property(e => e.CustomerPassWord)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CustomerUserName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.InvenProductId).HasColumnName("InvenProductID");

                entity.Property(e => e.InvenStoreId).HasColumnName("InvenStoreID");

                entity.HasOne(d => d.InvenProduct)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.InvenProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK2");

                entity.HasOne(d => d.InvenStore)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.InvenStoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK1");
            });

            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId1, e.OrderInvenId })
                    .HasName("PK__LineItem__015D7F4CE16C84A3");

                entity.ToTable("LineItem");

                entity.Property(e => e.OrderId1).HasColumnName("OrderID1");

                entity.HasOne(d => d.OrderId1Navigation)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.OrderId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK5");

                entity.HasOne(d => d.OrderInven)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.OrderInvenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK6");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderInvenId).HasColumnName("OrderInvenID");

                entity.HasOne(d => d.OrderAccount)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK3");

                entity.HasOne(d => d.OrderInven)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderInvenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK4");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductDescription).HasMaxLength(200);

                entity.Property(e => e.ProductGenere).HasMaxLength(200);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ProductPrice).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<StoreFront>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK__StoreFro__3B82F1012D20F47A");

                entity.ToTable("StoreFront");

                entity.Property(e => e.StoreAddress)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
