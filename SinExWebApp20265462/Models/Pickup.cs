using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("Pickup")]
    public class Pickup
    {
        [Key]
        public virtual int PickupId { get; set; }
        public virtual string Type { get; set; } // Immediate or Prearranged
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Province { get; set; }
        public virtual DateTime PickupDateTime { get; set; }

        [Required]
        public Shipment Shipment { get; set; }
        public virtual int WaybillId { get; set; }
    }
}