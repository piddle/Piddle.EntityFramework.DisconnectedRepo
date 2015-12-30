using Piddle.EntityFramework.DisconnectedRepo.TestingArea.EntityBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext
{
    [Table("FoodCourse", Schema = "dbo")]
    public class NormalFoodCourseEntity : FoodCourseEntityBase
    {
        [ForeignKey("MealId")]
        public NormalMealEntity Meal { get; set; }

        [ForeignKey("BillId")]
        public NormalBillEntity Bill { get; set; }
    }
}
