using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("Package")]
    public class Package
    {
        [Key]
        public virtual int PackageId { get; set; }

        public virtual PackageTypeSize PackageTypeSize { get; set; }

        public virtual string Description { get; set; }
        public virtual decimal Value { get; set; }

        public virtual float CustomerWeight { get; set; }
        public virtual float ActualWeight { get; set; }

        public virtual decimal PackageCost { get; set; }

        public virtual Shipment Shipment { get; set; }
    }
}