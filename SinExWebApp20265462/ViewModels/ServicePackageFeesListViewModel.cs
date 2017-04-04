using SinExWebApp20265462.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinExWebApp20265462.ViewModels
{
    public class ServicePackageFeesListViewModel
    {
        public virtual ServiceType ServiceType { get; set; }
        public virtual PackageType PackageType { get; set; }
    }
}