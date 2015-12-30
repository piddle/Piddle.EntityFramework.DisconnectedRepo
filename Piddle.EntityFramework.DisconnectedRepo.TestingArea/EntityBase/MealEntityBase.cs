using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.EntityBase
{
    public abstract class MealEntityBase
    {
        [Key]
        public long MealId { get; set; }

        public long BillId { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
