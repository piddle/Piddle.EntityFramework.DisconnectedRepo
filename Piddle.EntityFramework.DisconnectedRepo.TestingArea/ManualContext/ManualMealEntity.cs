using Piddle.EntityFramework.DisconnectedRepo.TestingArea.EntityBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.ManualContext
{
    [Table("Meal", Schema = "dbo")]
    public class ManualMealEntity : MealEntityBase
    {
        [ForeignKey("BillId")]
        public ManualBillEntity Bill { get; set; }

        public ICollection<ManualFoodCourseEntity> Courses { get; set; } // Note: Not virtual

        public ManualMealEntity()
        {
            // We definitely do not want to initialize the collections
            //Courses = new List<ManualFoodCourseEntity>();
        }
    }
}
