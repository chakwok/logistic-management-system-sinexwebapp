using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("ServicePackageFee")]
    public class ServicePackageFee
    {
        public virtual int ServicePackageFeeID { get; set; }
        public virtual decimal Fee { get; set; }
        public virtual decimal MinimumFee { get; set; }
        //Foreign key references PackageType
        public virtual int PackageTypeID { get; set; }
        //Foreign key references ServiceType
        public virtual int ServiceTypeID { get; set; }
        // Navigation property to PackageType
        public virtual PackageType PackageType { get; set; }
        // Navigation property to ServiceType
        public virtual ServiceType ServiceType { get; set; }
    }
}