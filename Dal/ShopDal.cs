using Avoda1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Avoda1.Dal
{
    public class ShopDal:DbContext
    {
        // Save in DataBase and get values from DataBase.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NewProduct>().ToTable("tblNewproducts");
            modelBuilder.Entity<Manger>().ToTable("tblLoginManger");
            modelBuilder.Entity<Customer>().ToTable("Table_2");
        }

        // Get objects type of NewProduct
        public DbSet<NewProduct> NewproductDB { get; set; }
        public DbSet<Manger> lgoinDb { get; set; }
        public DbSet<Customer> CustomerDB { get; set; }
    }
}