namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext.NormalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext.NormalDbContext";
        }

        protected override void Seed(Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext.NormalDbContext context)
        {
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
