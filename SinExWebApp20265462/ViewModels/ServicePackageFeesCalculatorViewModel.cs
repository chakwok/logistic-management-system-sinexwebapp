using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.ViewModels
{
    public class ServicePackageFeesCalculatorViewModel
    {
        public virtual string Origin { get; set; }
        public virtual string Destination { get; set; }
        public virtual List<SelectListItem> Destinations { get; set; }
        public virtual int ServiceTypeID { get; set; }
        public virtual List<SelectListItem> ServiceTypes { get; set; }
        public virtual int PackageTypeSizeID { get; set; }
        public virtual List<SelectListItem> PackageTypeSizes { get; set; }
        public virtual float Weight { get; set; }

        public virtual string CurrencyCode { get; set; }
        public virtual List<SelectListItem> CurrencyCodes { get; set; }

        public virtual decimal PackageCost { get; set; }
    }
}