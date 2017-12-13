using PandaTours.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
namespace PandaTours.DAL
{
    public class VacationDBContext : DbContext
    {
        //define tables in  project's data-babse

        //Customer table
       public DbSet<Customer> Customers { get; set; }
       //Orders table
       public DbSet<Order> Orders { get; set; }
       //VacationPackages table
       public DbSet<VacationPackage> VacationPackages { get; set; }
       //Suppliers table
       public DbSet<Supplier> Suppliers { get; set; }
       //Locations table
       public DbSet<Location> Locations { get; set; }

       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}