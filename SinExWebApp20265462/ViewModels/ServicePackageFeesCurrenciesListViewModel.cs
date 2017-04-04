using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.ViewModels
{
    public class ServicePackageFeesCurrenciesListViewModel
    {
        public virtual string CurrencyCode { get; set; }
        public virtual List<SelectListItem> CurrencyCodes { get; set; }
    }
}