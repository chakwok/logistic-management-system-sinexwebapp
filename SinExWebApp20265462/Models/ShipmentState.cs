using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("ShipmentState")]
    public class ShipmentState
    {
        [Key]
        public virtual int ShipmentStateID { get; set; }

        public virtual DateTime Time { get; set; }
        public virtual string Description { get; set; }
        public virtual string Location { get; set; }
        public virtual string Remarks { get; set; }

        public virtual Shipment Shipment { get; set; }
        public virtual int WaybillId { get; set; }
    }
}