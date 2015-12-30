using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.EntityBase
{
    public abstract class BillEntityBase
    {
        [Key]
        public long BillId { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
    }
}
