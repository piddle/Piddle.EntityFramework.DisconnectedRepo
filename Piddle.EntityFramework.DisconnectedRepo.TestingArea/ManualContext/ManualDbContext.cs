using System.Data.Entity;
using System.Diagnostics;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.ManualContext
{
    public class ManualDbContext : DbContext
    {
        public DbSet<ManualBillEntity> Bills { get; set; }
        public DbSet<ManualFoodCourseEntity> FoodCourse { get; set; }
        public DbSet<ManualMealEntity> Meals { get; set; }

        public ManualDbContext()
            : base("DefaultConnectionEF")
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.Log = (x) => Trace.Write(x);
        }
    }
}
