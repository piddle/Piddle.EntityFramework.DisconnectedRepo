using Piddle.EntityFramework.DisconnectedRepo.TestingArea.EntityBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.ManualContext
{
    [Table("FoodCourse", Schema = "dbo")]
    public class ManualFoodCourseEntity : FoodCourseEntityBase
    {
        [ForeignKey("MealId")]
        public ManualMealEntity Meal { get; set; }

        [ForeignKey("BillId")]
        public ManualBillEntity Bill { get; set; }
    }
}
