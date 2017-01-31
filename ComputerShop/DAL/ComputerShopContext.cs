using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ComputerShop.Models;


namespace ComputerShop.DAL
{
    public class ComputerShopContext : DbContext
    {
        //Nazwa kontekstu która będzie używana w ConnectionString
        public ComputerShopContext() : base("ComputerShopContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductFeedback> ProductFeedbacks { get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<OrderSupply> OrderSupplies { get; set; }
        public DbSet<OrderState> OrderStates{ get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderProductHistory> OrderProductHistory { get; set; }
        public DbSet<ProductOrderState> ProductOrderStates { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Regulation> Regulations { get; set; }
        public DbSet<Raport> Raports { get; set; }
        public DbSet<OrderUser> OrderUser { get; set; }


        //Zmiana nazw tabel z mnogich na pojedyncze
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Category>().
                HasOptional(e => e.ParentCategory).
                WithMany().
                HasForeignKey(m => m.ParentCategoryID);

        }


    }
}