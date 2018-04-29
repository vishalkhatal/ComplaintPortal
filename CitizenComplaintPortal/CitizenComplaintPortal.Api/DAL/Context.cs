using CitizensComplaintPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace CitizensComplaintPortal.DataAccess
{
    public class CitizensCompaintPortalContext : DbContext
    {

        public CitizensCompaintPortalContext() : base("CitizensCompaintPortalContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CitizensCompaintPortalContext, CitizenComplaintPortal.Api.Migrations.Configuration>());

        }

        public DbSet<User> users { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Comment> comments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<CitizensCompaintPortalContext,>());


        }
    }

}
