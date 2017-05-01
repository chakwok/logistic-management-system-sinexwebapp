using SinExWebApp20265462.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.ViewModels
{
    public class ArrangeShipmentPackageViewModel
    {
        public virtual int PackageTypeSizeID { get; set; }
        public virtual string PackageTypeSize { get; set; }
        public virtual List<SelectListItem> PackageTypeSizes { get; set; }

        [StringLength(70)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only characters and white spaces are accepted.")]
        public virtual string Description { get; set; }
        public virtual decimal Value { get; set; }

        public virtual float CustomerWeight { get; set; }

        public virtual decimal PackageCost { get; set; }

        public virtual Shipment Shipment { get; set; }
    }
}