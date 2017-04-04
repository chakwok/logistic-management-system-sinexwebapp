using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.ViewModels
{
    public class ServicePackageFeesIndexViewModel
    {
        public virtual ServicePackageFeesCurrenciesListViewModel Currency { get; set; }
        public virtual IEnumerable<ServicePackageFeesListViewModel> ServicePackageFees { get; set; }
    }
}