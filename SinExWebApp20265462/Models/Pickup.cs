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
        [StringLength(50)]
        public virtual string Building { get; set; }
        [Required]
        [StringLength(35)]
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string ProvinceCode { get; set; }    // Autogenerate
        public virtual string PostalCode { get; set; }
        public virtual DateTime PickupDateTime { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}