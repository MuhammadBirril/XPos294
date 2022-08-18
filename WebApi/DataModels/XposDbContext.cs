using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.DataModels
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category() { Id = 1, Initial = "MainCo", Name = "Main Course", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now },
                    new Category() { Id = 2, Initial = "Drink", Name = "Drink", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now },
                    new Category() { Id = 3, Initial = "Dessert", Name = "Dessert", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now }
                );

            modelBuilder.Entity<Variant>()
                .HasData(
                    new Variant() { Id = 1, CategoryId = 1, Initial = "Paket Nasi", Name = "Paket Nasi", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now },
                    new Variant() { Id = 2, CategoryId = 1, Initial = "Ala Carte", Name = "Ala Carte", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now },
                    new Variant() { Id = 3, CategoryId = 1, Initial = "Favorite", Name = "Favorite", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now },
                    new Variant() { Id = 4, CategoryId = 2, Initial = "Iced", Name = "Iced", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now }
                );

            modelBuilder.Entity<Product>()
                .HasData(
                    new Product() { Id = 1, VariantId = 1, Initial = "NasCap", Name = "Nasi Capcay", Price = 25000, Stock = 10, Description = "Capcay seafood", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now },
                    new Product() { Id = 2, VariantId = 3, Initial = "AyKal", Name = "Ayam Kalasan", Price = 26000, Stock = 10, Description = "Dengan bumbu rempah", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now },
                    new Product() { Id = 3, VariantId = 4, Initial = "KaMer", Name = "Iced Kacang Merah", Price = 18000, Stock = 10, Description = "Dengan gula aren", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now },
                    new Product() { Id = 4, VariantId = 2, Initial = "AyGor", Name = "Ayam Goreng", Price = 18000, Stock = 10, Description = "1/2 ekor", Active = true, CreateBy = "Admin", CreateDate = DateTime.Now }
                );

            modelBuilder.Entity<Account>()
                .HasData(
                    new Account() { Id = 1, UserName = "admin", Password = "admin1234", Active = true, FirstName = "Super", LastName = "User" },
                    new Account() { Id = 2, UserName = "user1", Password = "user1234", Active = true, FirstName = "User", LastName = "Satu" },
                    new Account() { Id = 3, UserName = "user2", Password = "user1234", Active = true, FirstName = "User", LastName = "Dua" }
                );


            modelBuilder.Entity<GroupJob>()
                .HasData(
                    new GroupJob() { Id = 1, Job = "Administrator", Active = true },
                    new GroupJob() { Id = 2, Job = "Supervisor", Active = true },
                    new GroupJob() { Id = 3, Job = "Staff", Active = true },
                    new GroupJob() { Id = 4, Job = "Cashier", Active = true }
                );

            modelBuilder.Entity<TransRole>()
                .HasData(
                    new TransRole() { Id = 1, GroupJobId = 2, Role = "Category" },
                    new TransRole() { Id = 2, GroupJobId = 2, Role = "Variant" },
                    new TransRole() { Id = 3, GroupJobId = 2, Role = "Product" },
                    new TransRole() { Id = 4, GroupJobId = 2, Role = "Order" },
                    new TransRole() { Id = 5, GroupJobId = 3, Role = "Category" },
                    new TransRole() { Id = 6, GroupJobId = 3, Role = "Variant" },
                    new TransRole() { Id = 7, GroupJobId = 3, Role = "Product" },
                    new TransRole() { Id = 8, GroupJobId = 4, Role = "Order" }
                );

            modelBuilder.Entity<UserRole>()
                 .HasData(
                     new UserRole() { Id = 1, UserName = "admin", GroupJobId = 1 },
                     new UserRole() { Id = 2, UserName = "user1", GroupJobId = 3 },
                     new UserRole() { Id = 3, UserName = "user2", GroupJobId = 4 }
                 );


        }
    }


    public class XposDbContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<FileCollection> FileCollections { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserRole> UserRoles  { get; set; }
        public DbSet<GroupJob> GroupJobs { get; set; }
        public DbSet<TransRole> TransRoles { get; set; }

        public DbSet<Book> Books { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            IConfigurationRoot builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(builder.GetConnectionString("DB_XPOS_CONN"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            //base.OnModelCreating(modelBuilder);
            //Category
            modelBuilder.Entity<Category>().HasIndex(o => o.Initial).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(o => o.Name).IsUnique();
            
            //Variant
            modelBuilder.Entity<Variant>().HasIndex(o => o.Initial).IsUnique();
            modelBuilder.Entity<Variant>().HasIndex(o => o.Name).IsUnique();


            //Products
            modelBuilder.Entity<Product>().HasIndex(o => o.Initial).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(o => o.Name).IsUnique();
            modelBuilder.Entity<Product>().Property(o => o.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(o => o.Stock).HasColumnType("decimal(18,2)");

            //OrderHeader
            modelBuilder.Entity<OrderHeader>().Property(o => o.Amount).HasColumnType("decimal(18,4)");

            //OrderDetail
            modelBuilder.Entity<OrderDetail>().Property(o => o.Quantity).HasColumnType("decimal(18,4)");
            modelBuilder.Entity<OrderDetail>().Property(o => o.Price).HasColumnType("decimal(18,4)");

            //FileCollection
            modelBuilder.Entity<FileCollection>().HasIndex(o => o.Title).IsUnique();
            modelBuilder.Entity<FileCollection>().HasIndex(o => o.FileName).IsUnique();

            //Accounts
            modelBuilder.Entity<Account>().HasIndex(o => o.UserName).IsUnique();

            modelBuilder.Entity<UserRole>()
                .HasIndex(o => new { o.UserName, o.GroupJobId })
                .IsUnique();

            modelBuilder.Entity<GroupJob>()
                .HasIndex(o => new { o.Job })
                .IsUnique();

            modelBuilder.Entity<TransRole>()
                .HasIndex(o => new { o.GroupJobId, o.Role })
                .IsUnique();

            modelBuilder.Entity<Book>()
               .HasIndex(o => o.KodeBuku)
               .IsUnique();

        }
    }
}