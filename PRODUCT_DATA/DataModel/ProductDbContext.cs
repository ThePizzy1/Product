
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PRODUCT_DATA.DataModel;
using PRODUCT_DOMAIN.DataModel;
using System;

namespace PRODUCT_DATA.DataModel
{

    public class ProductDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
        {
        }


        public virtual DbSet<ApplicationUser> User { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identity tablice
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");

            // Id nije radio zaboravila onaj dio da  mora to napravit itd
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CartId).IsRequired().HasMaxLength(50); 
                entity.HasOne<ApplicationUser>()
                      .WithMany() 
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
              
            });

           
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.CartId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.HasOne(e => e.Cart)
                      .WithMany() 
                      .HasForeignKey(e => e.CartId)
                      .HasPrincipalKey(c => c.CartId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.NumberOfItems).IsRequired().HasMaxLength(20);

                entity.HasOne(e => e.Product)
                      .WithMany() 
                      .HasForeignKey(e => e.ProductId)
                      .HasPrincipalKey(p => p.IdProduct)
                      .OnDelete(DeleteBehavior.Cascade);
            });

           
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("NEWID()");
                entity.Property(e => e.IdProduct).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Price).HasColumnType("decimal(20,2)");
                entity.Property(e => e.Weight).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Image).HasMaxLength(500);
                entity.Property(e => e.Keywords).HasMaxLength(500);
            });
        }


    }


    }
