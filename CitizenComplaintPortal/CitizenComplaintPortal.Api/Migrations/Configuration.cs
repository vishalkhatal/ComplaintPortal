namespace CitizenComplaintPortal.Api.Migrations
{
    using CitizensComplaintPortal.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CitizensComplaintPortal.DataAccess.CitizensCompaintPortalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CitizensComplaintPortal.DataAccess.CitizensCompaintPortalContext";
        }

        protected override void Seed(CitizensComplaintPortal.DataAccess.CitizensCompaintPortalContext context)
        {
            var helper = new User() { FirstName = "Vishal", LastName = "Khatal", MobileNo = "9049204092", EmailAddress = "vishkhatal@gmail.com", role = Roles.Helper };
            context.users.AddOrUpdate(helper);
            context.SaveChanges();
            var alternateHelper = new User() { FirstName = "Avinash", LastName = "Ravikumanr", MobileNo = "9049204092", EmailAddress = "avinash@gmail.com", role = Roles.Helper };
            context.users.AddOrUpdate(alternateHelper);
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
