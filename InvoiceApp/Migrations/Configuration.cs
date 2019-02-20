using InvoiceApp.Models;

namespace InvoiceApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InvoiceApp.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InvoiceApp.Models.MyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Customers.AddOrUpdate(p => p.Name,
                new Customer { Name = "Ahmad Othman", MobileNumber = "050-1234567" },
                new Customer { Name = "Samer Ahmad", MobileNumber = "050-1256547" },
                new Customer { Name = "Yaser Salah", MobileNumber = "050-5215678" },
                new Customer { Name = "Mohamad Yahia", MobileNumber = "050-5225678" }
            );


            context.Products.AddOrUpdate(p => p.Name,
                new Product { Name = "Tool Box", Price = 20 },
                new Product { Name = "Wrench", Price = 30 },
                new Product { Name = "Hammer", Price = 50 },
                new Product { Name = "Tape", Price = 5 }
            );

            context.SaveChanges();


        }
    }
}
