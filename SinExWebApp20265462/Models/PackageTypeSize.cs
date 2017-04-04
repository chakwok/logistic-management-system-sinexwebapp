using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.Models
{
    [Table("PackageTypeSize")]
    public class PackageTypeSize
    {
        public virtual int PackageTypeSizeID { get; set; }
        public virtual string Size { get; set; }
        public virtual int WeightLimit { get; set; }
        public virtual int PackageTypeID { get; set; }
        public virtual PackageType PackageType { get; set; }
    }
}