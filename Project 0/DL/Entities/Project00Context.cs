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
                entity.HasKey(e => new { e.CustomerId, e.CustomerStore })
                    .HasName("Acc_Store");

                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CustomerPassWord)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CustomerUserName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.InvenStoreId, e.InvenProductId })
                    .HasName("PKInventory_ID");

                entity.ToTable("Inventory");

                entity.Property(e => e.InvenStoreId).HasColumnName("InvenStoreID");

                entity.Property(e => e.InvenProductId).HasColumnName("InvenProductID");

                entity.HasOne(d => d.InvenProduct)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.InvenProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InvenProductID");

                entity.HasOne(d => d.InvenStore)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.InvenStoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InvenStoreID");
            });

            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.HasKey(e => new { e.LineOrderId, e.LineStoreId, e.LineInvenProdId })
                    .HasName("PKLineITem_ID");

                entity.ToTable("LineItem");

                entity.Property(e => e.LineOrderId).HasColumnName("LineOrderID");

                entity.Property(e => e.LineStoreId).HasColumnName("LineStoreID");

                entity.Property(e => e.LineInvenProdId).HasColumnName("LineInvenProdID");

                entity.HasOne(d => d.LineOrder)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.LineOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LineOrderID");

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => new { d.LineStoreId, d.LineInvenProdId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKInventory_ID");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderStoreId).HasColumnName("OrderStoreID");

                entity.Property(e => e.OrderTotal).HasColumnType("smallmoney");

                entity.HasOne(d => d.OrderNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => new { d.OrderAccountId, d.OrderStoreId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKAcc_Store");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductName).IsRequired();

                entity.Property(e => e.ProductPrice).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<StoreFront>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK__StoreFro__3B82F1011E9F17E0");

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
