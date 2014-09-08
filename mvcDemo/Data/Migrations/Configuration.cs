using Data.Models;

namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LeadContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            // Living dangerously!
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LeadContext context)
        {
            context.Leads.Add(
                new Lead {  FirstName = "Test", 
                            LastName = "testLast", 
                            LeadUser = "test@test.com", 
                            Address = new Address { AddressLine1 = "123 Front St.", 
                                                    City = "Boise", 
                                                    State = "ID", 
                                                    ZipCode = "83713" }, 
                            PhoneNumber = new TelephoneNumber { Number = "2085551212", 
                                                                NumberType = "Cell" } });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
