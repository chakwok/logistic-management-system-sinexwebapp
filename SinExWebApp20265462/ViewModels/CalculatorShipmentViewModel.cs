using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.ViewModels
{
    public class CalculatorShipmentViewModel
    {
        public virtual string Origin { get; set; }
        public virtual string Destination { get; set; }
        public virtual List<SelectListItem> Destinations { get; set; }
        public virtual int ServiceTypeID { get; set; }
        public virtual List<SelectListItem> ServiceTypes { get; set; }

        [Range(1, 10, ErrorMessage = "Please include 1-10 packages in your shipment.")]
        public virtual int NumberOfPackages { get; set; }
        public IList<CalculatorPackageViewModel> Packages { get; set; }

        public virtual string CurrencyCode { get; set; }
        public virtual List<SelectListItem> CurrencyCodes { get; set; }

        public virtual decimal ShipmentCost { get; set; }
    }
}