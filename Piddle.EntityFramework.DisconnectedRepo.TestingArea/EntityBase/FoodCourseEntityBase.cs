using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.EntityBase
{
    public abstract class FoodCourseEntityBase
    {
        [Key]
        public long FoodCourseId { get; set; }

        public long MealId { get; set; }

        public long BillId { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public FoodCourseType Type { get; set; }

        [Required]
        public decimal Cost { get; set; }
    }
}
