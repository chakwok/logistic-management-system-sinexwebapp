using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.ViewModels
{
    public class CalculatorPackageViewModel
    {
        [Key]
        public virtual int CalculatorPackageID { get; set; }
        public virtual int PackageTypeSizeID { get; set; }
        public virtual List<SelectListItem> PackageTypeSizes { get; set; }

        public virtual float Weight { get; set; }

        public virtual decimal PackageCost { get; set; }

        public CalculatorShipmentViewModel Shipment { get; set; }
    }
}