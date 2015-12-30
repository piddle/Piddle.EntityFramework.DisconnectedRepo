using Piddle.EntityFramework.DisconnectedRepo.TestingArea.EntityBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext
{
    [Table("Meal", Schema = "dbo")]
    public class NormalMealEntity : MealEntityBase
    {
        [ForeignKey("BillId")]
        public NormalBillEntity Bill { get; set; }

        public virtual ICollection<NormalFoodCourseEntity> Courses { get; set; }

        public NormalMealEntity()
        {
            Courses = new List<NormalFoodCourseEntity>();
        }
    }
}
