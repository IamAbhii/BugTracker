namespace Bug_Tracker_project.Migrations
{
    using Bug_Tracker_project.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bug_Tracker_project.Models.ApplicationDbContext>
    {
        ApplicationDbContext db;
        private UserHelper roleManager;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Bug_Tracker_project.Models.ApplicationDbContext";
            db = new ApplicationDbContext();
            roleManager = new UserHelper(db);
        }

        protected override void Seed(Bug_Tracker_project.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            roleManager.CreateNewRole("Admin");
            roleManager.CreateNewRole("ProjectManager");
            roleManager.CreateNewRole("Developer");
            roleManager.CreateNewRole("Submitter");
        }
    }
}
