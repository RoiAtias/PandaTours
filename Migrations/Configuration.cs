namespace PandaTours.Migrations
{
    using PandaTours.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PandaTours.DAL.VacationDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PandaTours.DAL.VacationDBContext";
        }

        protected override void Seed(PandaTours.DAL.VacationDBContext context)
        {
            var Suppliers = new List<Supplier>
            {
                new Supplier { SupplierID = 1,  SupplierName = "EshetTours", Address = "tel-aviv, israel", Destination = "Amsterdam", Email = "EshetTours@PandaTours.com", TelephoneSupplier = "03-9657452"},
                new Supplier { SupplierID = 2,  SupplierName = "GaliTours", Address = "Rishon-le-zion, israel", Destination = "Barcelona", Email = "GaliTours@PandaTours.com", TelephoneSupplier = "03-9678090" },
                new Supplier { SupplierID = 3,  SupplierName = "RimonTours", Address = "tel-aviv, israel", Destination = "Beijing", Email = "RimonTours@PandaTours.com", TelephoneSupplier = "03-9524343" },
                new Supplier { SupplierID = 4,  SupplierName = "MikaTours", Address = "ramat-gan, israel", Destination = "Dublin", Email = "MikaTours@PandaTours.com", TelephoneSupplier = "03-9426650" },
                new Supplier { SupplierID = 5,  SupplierName = "GefenTours", Address = "ramat-gan, israel", Destination = "London", Email = "GefenTours@PandaTours.com", TelephoneSupplier = "03-9805221" },
                new Supplier { SupplierID = 6,  SupplierName = "EesyGoTours", Address = "tel-aviv, israel", Destination = "New-York", Email = "EesyGoTours@PandaTours.com", TelephoneSupplier = "03-9662878" },
                new Supplier { SupplierID = 7,  SupplierName = "MonaTours", Address = "ramat-gan, israel", Destination = "Paris", Email = "MonaTours@PandaTours.com", TelephoneSupplier = "03-9426650" },
                new Supplier { SupplierID = 8,  SupplierName = "Daka90Tours", Address = "rishon-le-zion, israel", Destination = "Rome", Email = "Daka90Tours@PandaTours.com", TelephoneSupplier = "03-9657391" },
                new Supplier { SupplierID = 9,  SupplierName = "IsstaTours", Address = "ramat-gan, israel", Destination = "Berlin", Email = "IsstaTours@PandaTours.com", TelephoneSupplier = "03-9887033" },
            };
            Suppliers.ForEach(s => context.Suppliers.AddOrUpdate(p => p.SupplierName, s));
            context.SaveChanges();

            var VacationPackages = new List<VacationPackage>
            {
                new VacationPackage() { VacationPackageID = 1, SupplierID = 1, Destination = "Amsterdam", Hotel = "Hotel Okura Amsterdam", DepartDate = DateTime.Parse("2015-09-01"), ReturnDate = DateTime.Parse("2015-09-08"), NightNum = 7, SinglePay = 399.9 },
                new VacationPackage() { VacationPackageID = 2, SupplierID = 2, Destination = "Barcelona", Hotel = "Hotel Arts Barcelona", DepartDate = DateTime.Parse("2015-09-03"), ReturnDate = DateTime.Parse("2015-09-09"), NightNum = 6, SinglePay = 299.9 },
                new VacationPackage() { VacationPackageID = 3, SupplierID = 3, Destination = "Beijing", Hotel = "New World Beijing Hotel", DepartDate = DateTime.Parse("2015-09-03"), ReturnDate = DateTime.Parse("2015-09-10"), NightNum = 7, SinglePay = 699.9 },
                new VacationPackage() { VacationPackageID = 4, SupplierID = 4, Destination = "Dublin", Hotel = "InterContinental Dublin", DepartDate = DateTime.Parse("2015-09-02"), ReturnDate = DateTime.Parse("2015-09-06"), NightNum = 4, SinglePay = 359.9 },
                new VacationPackage() { VacationPackageID = 5, SupplierID = 5, Destination = "London", Hotel = "Four Seasons Hotel London", DepartDate = DateTime.Parse("2015-09-05"), ReturnDate = DateTime.Parse("2015-09-10"), NightNum = 6, SinglePay = 499.9 },
                new VacationPackage() { VacationPackageID = 6, SupplierID = 6, Destination = "New-York", Hotel = "Conrad New York ", DepartDate = DateTime.Parse("2015-09-01"), ReturnDate = DateTime.Parse("2015-09-08"), NightNum = 7, SinglePay = 999.9 },
                new VacationPackage() { VacationPackageID = 7, SupplierID = 7, Destination = "Paris", Hotel = "Hotel Du Louvre", DepartDate = DateTime.Parse("2015-09-02"), ReturnDate = DateTime.Parse("2015-09-08"), NightNum = 6, SinglePay = 699.9 },
                new VacationPackage() { VacationPackageID = 8, SupplierID = 8, Destination = "Rome", Hotel = "Grand Hotel Palace", DepartDate = DateTime.Parse("2015-09-04"), ReturnDate = DateTime.Parse("2015-09-08"), NightNum = 4, SinglePay = 299.9 },
                new VacationPackage() { VacationPackageID = 9, SupplierID = 9, Destination = "Berlin", Hotel = "The Westin Grand Berlin", DepartDate = DateTime.Parse("2015-09-05"), ReturnDate = DateTime.Parse("2015-09-11"), NightNum = 7, SinglePay = 499.9 },
            };
            VacationPackages.ForEach(s => context.VacationPackages.AddOrUpdate(p => p.VacationPackageID, s));
            context.SaveChanges();

            var Customers = new List<Customer>
            {
                new Customer { CustomerID = 1, PassportNum = "845790032", FirstName = "Roi", LastName = "Atias", BirthDate = DateTime.Parse("1980-09-10"), Address = "Israel", PhoneNum = "0544274941", Email = "roiAtias@PandaTours.com", JoinDate = DateTime.Parse("2015-05-11") },
                new Customer { CustomerID = 2, PassportNum = "561034421", FirstName = "Shirel", LastName = "Ashkenazi", BirthDate = DateTime.Parse("1985-01-15"), Address = "Israel", PhoneNum = "0549469695", Email = "shirelAshkenazi@PandaTours.com", JoinDate = DateTime.Parse("2015-05-11") },
                new Customer { CustomerID = 3, PassportNum = "792093128", FirstName = "Michal", LastName = "Cohen", BirthDate = DateTime.Parse("1983-08-25"), Address = "Israel", PhoneNum = "0523458871", Email = "michalik@PandaTours.com", JoinDate = DateTime.Parse("2014-06-18") },
                new Customer { CustomerID = 4, PassportNum = "445790032", FirstName = "Mika", LastName = "Atias", BirthDate = DateTime.Parse("1980-07-15"), Address = "Israel", PhoneNum = "0544274941", Email = "Mika@PandaTours.com", JoinDate = DateTime.Parse("2015-05-12") },
                new Customer { CustomerID = 5, PassportNum = "661034421", FirstName = "Rina", LastName = "hazan", BirthDate = DateTime.Parse("1985-02-25"), Address = "Israel", PhoneNum = "0549469695", Email = "Rina@PandaTours.com", JoinDate = DateTime.Parse("2015-05-13") },
                new Customer { CustomerID = 6, PassportNum = "992093128", FirstName = "chen", LastName = "Ashkenazi", BirthDate = DateTime.Parse("1983-09-26"), Address = "Israel", PhoneNum = "0523458871", Email = "chen@PandaTours.com", JoinDate = DateTime.Parse("2015-05-14") },
            };
            Customers.ForEach(s => context.Customers.AddOrUpdate(p => p.CustomerID, s));
            context.SaveChanges();
        }
    }
}