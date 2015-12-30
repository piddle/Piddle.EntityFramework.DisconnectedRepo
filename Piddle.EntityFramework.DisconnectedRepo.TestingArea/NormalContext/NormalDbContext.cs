using System.Data.Entity;
using System.Diagnostics;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext
{
    public class NormalDbContext : DbContext
    {
        public DbSet<NormalBillEntity> Bills { get; set; }
        public DbSet<NormalFoodCourseEntity> FoodCourse { get; set; }
        public DbSet<NormalMealEntity> Meals { get; set; }

        public NormalDbContext()
            : base("DefaultConnectionEF")
        {
            Database.Log = (x) => Trace.Write(x);
        }
    }
}
