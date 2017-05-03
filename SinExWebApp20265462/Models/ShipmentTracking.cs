using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("ShipmentTracking")]
    public class ShipmentTracking
    {
        [Key]
        public virtual int ShipmentTrackingID { get; set; }
        
        [Required]
        public virtual Shipment Shipment { get; set; }
        public virtual int WaybillId { get; set;}

        public virtual ICollection<ShipmentState> ShipmentStates { get; set; }
    }
}