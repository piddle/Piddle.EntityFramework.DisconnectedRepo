using Piddle.EntityFramework.DisconnectedRepo.TestingArea.EntityBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext
{
    [Table("Bill", Schema = "dbo")]
    public class NormalBillEntity : BillEntityBase
    {
        public virtual ICollection<NormalMealEntity> Meals { get; set; }
        public virtual ICollection<NormalFoodCourseEntity> Courses { get; set; }

        public NormalBillEntity()
        {
            Meals = new List<NormalMealEntity>();
            Courses = new List<NormalFoodCourseEntity>();
        }
    }
}
