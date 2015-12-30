using Piddle.EntityFramework.DisconnectedRepo.TestingArea.EntityBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.ManualContext
{
    [Table("Bill", Schema = "dbo")]
    public class ManualBillEntity : BillEntityBase
    {
        public ICollection<ManualMealEntity> Meals { get; set; } // Note: Not virtual
        public ICollection<ManualFoodCourseEntity> Courses { get; set; } // Note: Not virtual

        public ManualBillEntity()
        {
            // We definitely do not want to initialize the collections
            //Meals = new List<ManualMealEntity>();
            //Courses = new List<ManualFoodCourseEntity>();
        }
    }
}
